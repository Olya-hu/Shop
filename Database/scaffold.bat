cd %~dp0
dotnet ef dbcontext scaffold "Server=localhost;Database=shop;User=root;Password=root;" "Pomelo.EntityFrameworkCore.MySql" -c ShopContext -f -d --no-build -p "Database.csproj"
pause
