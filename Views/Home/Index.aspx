<%@ Page Language="C#" MasterPageFile="~/Themes/Default/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function GetQS(ji, defaultValue) {
            var hu = window.location.search.substring(1);
            var gy = hu.split("&");
            for (var i = 0; i < gy.length; i++) {
                var ft = gy[i].split("=");
                if (ft[0] == ji) {
                    return ft[1];
                }
            }
            return defaultValue;
        }

        $(document).ready(function () {
            $("#btnSubmit").button();
            $("#btnSubmit").click(function () {
                $("#litFullUrl").html('Loading...');

                $.ajax({
                    url: '../../API/json.svc/create?fullurl=' + $("#tbUrl").val(),
                    success: function (data) {
                        if (data.ErrorCode == 0) {
                            $("#litFullUrl").html("<a href='" + data.Data.TinyUrl + "'>" + data.Data.TinyUrl + "</a>");
                        }
                        else {
                            $("#litFullUrl").html(data.Message);
                        }
                    }
                });
            });

            var url = GetQS('url');

            if (url != undefined && url != null) {
                $("#tbUrl").val(unescape(url));
                $("#btnSubmit").click();
            }
        });
    </script>
    
    <table class="input-wrapper">
        <tr>
            <td><input id="tbUrl" type="text" class="input" value="http://" /></td>
            <td>&nbsp;</td>
            <td><input id="btnSubmit" type="button" value="Shove" /></td>
        </tr>
        <tr>
            <td colspan="3">
                <div id="litFullUrl"></div>
            </td>
        </tr>
    </table>
</asp:Content>
