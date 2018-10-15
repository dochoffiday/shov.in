using System;
using AJ.UtiliTools;
using Shovin.Models;
using Shovin.DB;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Shovin.API
{
    public class Business
    {
        public static Result<ShoveWrapper> Create(String Url, String Keyword)
        {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            String ip = endpoint.Address;

            Result<Shove> result = ShoveDAL.Create(Helper.UrlDecode(Url), Helper.UrlDecode(Keyword), ip);

            if (result.Data != null)
            {
                result.Data.TinyUrl = "{0}{1}".F(UtiliSetting.AppSetting("ServiceUrl"), result.Data.TinyUrl);
            }

            return GetResult(result);
        }

        public static Result<ShoveWrapper> Get(String Url)
        {
            Result<Shove> result = ShoveDAL.GetByTinyUrl(Helper.UrlDecode(Url));

            if (result.Data != null)
            {
                result.Data.TinyUrl = "{0}{1}".F(UtiliSetting.AppSetting("ServiceUrl"), result.Data.TinyUrl);
            }

            return GetResult(result);
        }

        protected static Result<ShoveWrapper> GetResult(Result<Shovin.DB.Shove> Shove)
        {
            return new Result<ShoveWrapper>(Shove.ErrorCode, Shove.Message, new ShoveWrapper(Shove.Data));
        }
    }
}