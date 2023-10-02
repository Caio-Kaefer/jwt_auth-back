
using AuthAPI.Interfaces;

namespace AuthAPI.Tokens
{
    public class Token 
    {
        public string JwtKey()
        {
            var key = "80f2f6c05721348e0e122c5813198c4 ";
            return key;
        }

        public string JwtIssuer()
        {
            var issuer = "Caio";
            return issuer;
        }

        public string JwtAudience()
        {
            var audience = "";
            return audience;
        }

        public string JwtClaim(string claim)
        {
            var _claim = claim;
            return _claim;
        }

        public int JwtExpiry()
        {
                var expiry = 432000;
                return expiry;
            }
        }
    }
