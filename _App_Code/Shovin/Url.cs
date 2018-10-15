using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shovin
{
    public class Url
    {
        public static String TinyUrl(int Number)
        {
            String chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_~";
            int baseNumber = chars.Length;

            int r;
            String newNumber = "";

            // in r we have the offset of the char that was converted to the new base
            while (Number >= baseNumber)
            {
                r = Number % baseNumber;
                newNumber = chars[r] + newNumber;
                Number = Number / baseNumber;
            }
            // the last number to convert
            newNumber = chars[Number] + newNumber;

            return newNumber;
        }
    }
}