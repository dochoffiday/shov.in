using System;

namespace Shovin.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "xml" in code, svc and config file together.
    public class xml : Ixml
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
