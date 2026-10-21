using System.Security.Cryptography;
using System.Text;

namespace NguyenHuuDung_0299.Services;

public interface IOtpService
{
    string GenerateCode();
    string Hash(string code);
    bool Verify(string code, string hash);
}

public sealed class OtpService : IOtpService
{
    public string GenerateCode() => RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

    public string Hash(string code)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(code)));

    public bool Verify(string code, string hash)
        => CryptographicOperations.FixedTimeEquals(
            SHA256.HashData(Encoding.UTF8.GetBytes(code)), Convert.FromHexString(hash));
}
