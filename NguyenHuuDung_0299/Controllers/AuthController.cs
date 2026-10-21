using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenHuuDung_0299.Data;
using NguyenHuuDung_0299.Models.Auth;
using NguyenHuuDung_0299.Models.Users;
using NguyenHuuDung_0299.Services;

namespace NguyenHuuDung_0299.Controllers;

[ApiController, Route("api/auth")]
public class AuthController(AppDbContext db, UserManager<User> userManager,
    IOtpService otpService, IEmailService emailService, ITokenService tokenService) : ControllerBase
{
    private const int OtpLifetimeMinutes = 5;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var identifier = request.Username.Trim();
        var user = await userManager.FindByNameAsync(identifier)
                ?? await userManager.FindByEmailAsync(identifier);

        if (user is null || !user.IsActive || !user.IsEmailVerified ||
            !await userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized("Thông tin đăng nhập không hợp lệ hoặc tài khoản chưa kích hoạt.");

        var code = otpService.GenerateCode();
        db.LoginOtps.Add(new LoginOtp { UserId = user.Id, CodeHash = otpService.Hash(code), ExpiresAtUtc = DateTime.UtcNow.AddMinutes(OtpLifetimeMinutes) });
        await db.SaveChangesAsync(cancellationToken);
        await emailService.SendOtpAsync(user.Email!, code, "xác minh đăng nhập", cancellationToken);
        return Accepted(new { message = "OTP đăng nhập đã được gửi đến email." });
    }

    [HttpPost("login/verify")]
    public async Task<IActionResult> VerifyLogin(VerifyLoginRequest request, CancellationToken cancellationToken)
    {
        var identifier = request.Username.Trim();
        var user = await userManager.FindByNameAsync(identifier)
                ?? await userManager.FindByEmailAsync(identifier);

        var otp = user is null ? null : await db.LoginOtps.Where(x => x.UserId == user.Id && x.UsedAtUtc == null)
            .OrderByDescending(x => x.ExpiresAtUtc).FirstOrDefaultAsync(cancellationToken);
        if (user is null || !user.IsActive || !user.IsEmailVerified || otp is null || otp.ExpiresAtUtc < DateTime.UtcNow || !otpService.Verify(request.Code, otp.CodeHash))
            return Unauthorized("OTP không hợp lệ hoặc đã hết hạn.");
        otp.UsedAtUtc = DateTime.UtcNow;
        user.LastLoginAtUtc = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        var roles = await userManager.GetRolesAsync(user);
        return Ok(new { token = tokenService.CreateToken(user, roles), roles });
    }
}
