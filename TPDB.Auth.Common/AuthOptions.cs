using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace TPDB.Auth.Common
{
    public class AuthOptions
    {
        public string Issuer { get; set; } //тот, кто сгенерировал токен
        public string Audience { get; set; } //тот, для кого предназначался токен
        public string Secret { get; set; } //секретный ключ
        public int TokenLifetime { get; set; } //секунды
        
        public SymmetricSecurityKey GetSymetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
