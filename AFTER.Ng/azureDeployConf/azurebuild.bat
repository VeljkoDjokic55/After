powershell -Command "& {ng build --prod; Copy-Item "web.config" -Destination "..\..\AFTER.WebApp\wwwroot\dist"}"
pause