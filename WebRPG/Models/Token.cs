using System;

namespace WebRPG.Models
{
    public class Token
    {
        public string Id { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Token()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}