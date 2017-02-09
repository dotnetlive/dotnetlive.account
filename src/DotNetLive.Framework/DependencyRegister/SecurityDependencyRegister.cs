using DotNetLive.Framework.DependencyManagement;
using DotNetLive.Framework.Models;
using DotNetLive.Framework.UserIdentity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetLive.Framework.DependencyRegister
{
    public class SecurityDependencyRegister : IDependencyRegister
    {
        public void Register(IServiceCollection services, IConfigurationRoot configuration,
            IHostingEnvironment hostingEnvironment, IOptions<SecuritySettings> securitySettings)
        {
            services.Configure<SecuritySettings>(configuration.GetSection("SecuritySettings"));

            var _securitySettings = services.BuildServiceProvider().GetService<IOptions<SecuritySettings>>()?.Value;

            services.Configure<IdentityOptions>(options =>
            {
                var dataProtectionPath = Path.Combine(_securitySettings.DataProtectionPath);

                var applicationCookie = options.Cookies.ApplicationCookie;
                applicationCookie.AuthenticationScheme = "ApplicationCookie";
                applicationCookie.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(dataProtectionPath));
                applicationCookie.CookieDomain = _securitySettings.DomainName;
                applicationCookie.CookieName = "dnl-auth";
                applicationCookie.CookieHttpOnly = false;
                applicationCookie.Events = new CookieAuthenticationEvents();

                options.Lockout.AllowedForNewUsers = true;

                options.SecurityStampValidationInterval = TimeSpan.FromSeconds(30);
                if (options.OnSecurityStampRefreshingPrincipal == null)
                {
                    options.OnSecurityStampRefreshingPrincipal = SecurityStampValidatorCallback.UpdatePrincipal;
                }
            });

            // Services used by identity
            services.AddAuthentication(options =>
            {
                // This is the Default value for ExternalCookieAuthenticationScheme
                options.SignInScheme = new IdentityCookieOptions().ApplicationCookie.AuthenticationScheme;
            });

            services.TryAddScoped<IdentityMarkerService>();
            services.TryAddScoped<IUserValidator<ApplicationUser>, UserValidator<ApplicationUser>>();
            services.TryAddScoped<IPasswordValidator<ApplicationUser>, PasswordValidator<ApplicationUser>>();
            services.TryAddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser>>();
            services.TryAddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            services.TryAddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();

            AddDefaultTokenProviders(services);
        }

        private void AddDefaultTokenProviders(IServiceCollection services)
        {
            var dataProtectionProviderType = typeof(DataProtectorTokenProvider<>).MakeGenericType(typeof(ApplicationUser));
            var phoneNumberProviderType = typeof(PhoneNumberTokenProvider<>).MakeGenericType(typeof(ApplicationUser));
            var emailTokenProviderType = typeof(EmailTokenProvider<>).MakeGenericType(typeof(ApplicationUser));
            AddTokenProvider(services, TokenOptions.DefaultProvider, dataProtectionProviderType);
            AddTokenProvider(services, TokenOptions.DefaultEmailProvider, emailTokenProviderType);
            AddTokenProvider(services, TokenOptions.DefaultPhoneProvider, phoneNumberProviderType);
        }

        private void AddTokenProvider(IServiceCollection services, string providerName, Type provider)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Tokens.ProviderMap[providerName] = new TokenProviderDescriptor(provider);
            });

            services.AddSingleton(provider);
        }
    }
    public class SecurityStampValidatorCallback
    {
        public static Task UpdatePrincipal(SecurityStampRefreshingPrincipalContext context)
        {
            var newClaimTypes = context.NewPrincipal.Claims.Select(x => x.Type);
            var currentClaimsToKeep = context.CurrentPrincipal.Claims.Where(x => !newClaimTypes.Contains(x.Type));

            var id = context.NewPrincipal.Identities.First();
            id.AddClaims(currentClaimsToKeep);

            return Task.FromResult(0);
        }
    }
}
