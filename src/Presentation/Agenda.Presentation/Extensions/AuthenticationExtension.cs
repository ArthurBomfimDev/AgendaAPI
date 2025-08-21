//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//namespace Agenda.Presentation.Extensions;

//public static class AuthenticationExtension
//{
//    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
//    {
//        var jwtKey = configuration["Jwt:Key"];
//        if (string.IsNullOrEmpty(jwtKey))
//        {
//            throw new InvalidOperationException("A chave JWT (Jwt:Key) não está configurada.");
//        }

//        services.AddAuthentication(options =>
//        {
//            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//        }).AddJwtBearer(options =>
//        {
//            options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
//                ValidateIssuer = false,
//                ValidateAudience = false,
//                ValidateLifetime = true
//            };
//        });

//        services.AddAuthorization();

//        return services;
//    }
//}