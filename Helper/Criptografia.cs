﻿using System.Text;
using System.Security.Cryptography;

namespace SistemaRedeWork.Models {
    public static class Criptografia {
        public static string GerarHash(this string valor) {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(valor);


            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array) {
                strHexa.Append(item.ToString("X2"));
            }
            return strHexa.ToString();
        }
    }
}
