using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace library_management_system
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddJwtBearer("LibrarianClient", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myunlegivelibrariansecret")),
                        ValidAudience = "AudienceClientLibrarian",
                        ValidIssuer = "IssuerClientLibrarian",
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                     };
                })
                .AddJwtBearer("StudentClient", options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myunlegivestudentsecret")),
                         ValidAudience = "AudienceClientstudent",
                         ValidIssuer = "IssuerClientstudent",
                         ValidateIssuerSigningKey = true,
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero
                     };
                 });
            services.AddAuthorization();
            services.AddControllers();
        }
    }
}
