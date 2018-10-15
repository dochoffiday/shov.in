using System;

namespace Shovin
{
    public class ShoveWrapper
    {
        public String FullUrl { get; set; }
        public String TinyUrl { get; set; }

        public ShoveWrapper()
        {
        }

        public ShoveWrapper(Shovin.DB.Shove shove)
        {
            if (shove != null)
            {
                FullUrl = shove.FullUrl;
                TinyUrl = shove.TinyUrl;
            }
        }
    }
}