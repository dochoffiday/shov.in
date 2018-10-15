using System;

namespace Shovin.API
{
    public class json : Ijson
    {
        public Result<ShoveWrapper> create(string url, String keyword)
        {
            return Shovin.API.Business.Create(url, keyword);
        }

        public Result<ShoveWrapper> get(string url)
        {
            return Shovin.API.Business.Get(url);
        }
    }
}
