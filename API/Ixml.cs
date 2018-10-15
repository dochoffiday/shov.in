using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Shovin.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Ijson" in both code and config file together.
    [ServiceContract]
    public interface Ixml
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/create?fullurl={url}&keyword={keyword}")]
        Result<ShoveWrapper> create(String url, String keyword);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/get?tinyurl={url}")]
        Result<ShoveWrapper> get(String url);
    }
}
