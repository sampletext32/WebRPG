namespace WebRPG.Services
{
    public interface ITokenService
    {
        public string Create();

        public bool Check(string tokenId);
    }
}