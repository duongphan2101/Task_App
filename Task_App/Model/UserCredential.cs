using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    [Serializable]
    public class UserCredential
    {
        public int UserId { get; set; }
        public byte[] EncryptedPassword { get; set; }

        public void SetPassword(string plain)
        {
            EncryptedPassword = CryptoHelper.EncryptString(plain);
        }

        public string GetPassword()
        {
            return CryptoHelper.DecryptString(EncryptedPassword);
        }
    }
}
