using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round.Online.Luncher.Cs
{
    class Get_Uid
    {
        public static string Get_Uid_Func(int port,string room_name)
        {
            byte[] dataToEncode = System.Text.Encoding.UTF8.GetBytes(room_name + "|" +Generate(10)+"|"+port.ToString());
            string base64EncodedString = Convert.ToBase64String(dataToEncode);

            string return_string = "ROL-"+base64EncodedString;

            return return_string;
        }

        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,:;()[]!?*+-";
        private static readonly Random Random = new Random();

        public static string Generate(int length)
        {
            return new string(Enumerable.Repeat(Chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
