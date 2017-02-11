using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotNetLive.AccountApi
{
    // https://gist.github.com/pmhsfelix/4151369
    public class MyTokenHandler : Microsoft.IdentityModel.Tokens.ISecurityTokenValidator
    {
        private int m_MaximumTokenByteSize;

        public MyTokenHandler()
        { }

        bool ISecurityTokenValidator.CanValidateToken
        {
            get
            {
                // throw new NotImplementedException();
                return true;
            }
        }



        int ISecurityTokenValidator.MaximumTokenSizeInBytes
        {
            get
            {
                return this.m_MaximumTokenByteSize;
            }

            set
            {
                this.m_MaximumTokenByteSize = value;
            }
        }

        bool ISecurityTokenValidator.CanReadToken(string securityToken)
        {
            System.Console.WriteLine(securityToken);
            return true;
        }

        ClaimsPrincipal ISecurityTokenValidator.ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // validatedToken = new JwtSecurityToken(securityToken);
            try
            {

                tokenHandler.ValidateToken(securityToken, validationParameters, out validatedToken);
                validatedToken = new JwtSecurityToken("jwtEncodedString");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }



            ClaimsPrincipal principal = null;
            // SecurityToken validToken = null;

            validatedToken = null;


            System.Collections.Generic.List<Claim> ls = new System.Collections.Generic.List<Claim>();

            ls.Add(new Claim(ClaimTypes.Name, "IcanHazUsr_éèêëïàáâäåãæóòôöõõúùûüñçø_ÉÈÊËÏÀÁÂÄÅÃÆÓÒÔÖÕÕÚÙÛÜÑÇØ 你好，世界 Привет\tмир", ClaimValueTypes.String)
            );

            // 

            ClaimsIdentity id = new ClaimsIdentity("authenticationType");
            id.AddClaims(ls);

            principal = new ClaimsPrincipal(id);

            return principal;
            throw new NotImplementedException();
        }


    }
}
