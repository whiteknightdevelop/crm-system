namespace Petadmin.Repository.Cryptography
{
    public interface IEncryptService
    {
        byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv);
        string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv);
    }
}
