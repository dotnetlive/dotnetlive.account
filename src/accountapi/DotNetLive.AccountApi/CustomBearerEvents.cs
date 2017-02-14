using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Threading.Tasks;

namespace DotNetLive.AccountApi
{
    // https://github.com/aspnet/Security/blob/master/src/Microsoft.AspNetCore.Authentication.JwtBearer/Events/JwtBearerEvents.cs
    public class CustomBearerEvents : Microsoft.AspNetCore.Authentication.JwtBearer.IJwtBearerEvents
    {

        /// <summary>
        /// Invoked if exceptions are thrown during request processing. The exceptions will be re-thrown after this event unless suppressed.
        /// </summary>
        public Func<AuthenticationFailedContext, Task> OnAuthenticationFailed { get; set; } = context => Task.FromResult(0);

        /// <summary>
        /// Invoked when a protocol message is first received.
        /// </summary>
        public Func<MessageReceivedContext, Task> OnMessageReceived { get; set; } = context => Task.FromResult(0);

        /// <summary>
        /// Invoked after the security token has passed validation and a ClaimsIdentity has been generated.
        /// </summary>
        public Func<TokenValidatedContext, Task> OnTokenValidated { get; set; } = context => Task.FromResult(0);


        /// <summary>
        /// Invoked before a challenge is sent back to the caller.
        /// </summary>
        public Func<JwtBearerChallengeContext, Task> OnChallenge { get; set; } = context => Task.FromResult(0);


        Task IJwtBearerEvents.AuthenticationFailed(AuthenticationFailedContext context)
        {
            return OnAuthenticationFailed(context);
        }

        Task IJwtBearerEvents.Challenge(JwtBearerChallengeContext context)
        {
            return OnChallenge(context);
        }

        Task IJwtBearerEvents.MessageReceived(MessageReceivedContext context)
        {
            return OnMessageReceived(context);
        }

        Task IJwtBearerEvents.TokenValidated(TokenValidatedContext context)
        {
            return OnTokenValidated(context);
        }
    }
}
