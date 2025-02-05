using System.Security.Cryptography;
using System.Text;

namespace Security.Encryption;

public static class AesEncryption
{
    public static string Encrypt(string text)
    {
        const string key = "b14ca5898a4e4klinmer2ea2315a1916";
        var iv = new byte[16];
        byte[] array;
        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            //var alg = HashAlgorithm.Create("SHA512");
            //var keyHash = alg?.ComputeHash(Encoding.UTF8.GetBytes(key));
            //aes.Key = keyHash.Take(24).ToArray();
            //aes.IV = iv;


            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(text);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public static string Decrypt(string encrypted)
    {
        const string key = "b14ca5898a4e4klinmer2ea2315a1916";
        var iv = new byte[16];
        var buffer = Convert.FromBase64String(encrypted);
        using var aes = Aes.Create();

        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = iv;

        //var alg = HashAlgorithm.Create("SHA512");
        //var keyHash = alg?.ComputeHash(Encoding.UTF8.GetBytes(key));
        //aes.Key = keyHash.Take(24).ToArray();
        //aes.IV = iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }
}