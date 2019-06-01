using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility
{
    public class ROT13Decoder
    {
        public static string Decode(string ciphertext)
        {
            StringBuilder plaintext = new StringBuilder();

            foreach(char letter in ciphertext)
            {
                //velika slova 65(A)-90(Z), mala slova 97(a)-122(z)
                int numVal = (int)letter;
                int shifted = numVal;
                //T:\Sberafvp\Jverfunex\Jverfunex.rkr
                //G:\Forensic\Wireshark\Wireshark.exe
                if (numVal >=65 && numVal <= 90)
                {
                    shifted = (numVal - 13 < 65) ? (numVal + 13) : numVal - 13;
                } else if (numVal >= 97 && numVal <= 122)
                {
                    shifted = (numVal - 13 < 97) ? (numVal + 13) : numVal - 13;
                }

                plaintext.Append((char)shifted);
            }

            return plaintext.ToString();
        }
    }
}
