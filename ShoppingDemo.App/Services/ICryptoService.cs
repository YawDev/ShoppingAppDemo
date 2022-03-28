namespace ShoppingDemo.App.Services
{
    public interface ICryptoService
    {
        string Encrypt(string data);

        string Decrypt(string data);
    }

    public class CryptoService : ICryptoService
    {
        public string Decrypt(string data)
        {
            throw new System.NotImplementedException();
        }

        public string Encrypt(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}