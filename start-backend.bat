@echo off
title ASP.NET Core Backend (Port 5007)
echo ===================================================
echo   Khoi dong ASP.NET Core Web API tren Port 5007...
echo ===================================================
cd /d "%~dp0NguyenHuuDung_0299"
dotnet run --launch-profile http
pause
