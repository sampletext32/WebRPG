using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRPG.Models;

namespace WebRPG.Services
{
    public class TokenService : ITokenService
    {
        private readonly List<Token> _activeTokens;

        public TokenService()
        {
            _activeTokens = new List<Token>();
        }

        public string Create()
        {
            var token = new Token {ExpirationDate = DateTime.Now.AddSeconds(10)};
            _activeTokens.Add(token);
            return token.Id;
        }

        public bool Check(string tokenId)
        {
            var token = _activeTokens.Find(t => t.Id == tokenId);

            if (token == null)
            {
                return false;
            }

            if (token.ExpirationDate < DateTime.Now)
            {
                _activeTokens.Remove(token);
                return false;
            }

            token.ExpirationDate = DateTime.Now.AddSeconds(10);
            return true;
        }
    }
}