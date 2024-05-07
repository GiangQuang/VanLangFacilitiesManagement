
using System.Security.Cryptography;
using System.Text;
using VLFM.Services.Interfaces;

namespace VLFM.Services
{
    public class PasswordService : IPasswordService
    {
        public string EncryptPassword(string Password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(Password);
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                //Convert byte array to hex string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                string hashedPassword = sb.ToString();
                hashedPassword = hashedPassword.Insert(3, "YuY");
                hashedPassword = hashedPassword.Insert(5, "Bf");
                return hashedPassword;
            }
        }
    }
}
