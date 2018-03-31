using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;

namespace WebViews.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            //https://developers.facebook.com/docs/messenger-platform/webview
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            var message = activity.CreateReply();

            message.ChannelData = JObject.Parse(@"
            {
                ""attachment"": {
                ""type"": ""template"",
                ""payload"": {
                            ""template_type"": ""button"",
                    ""text"": ""Your 🍕 is on it's way!"",
                    ""buttons"": [
                    {
                        ""type"": ""web_url"",
                        ""url"": ""https://webviewtest2103.azurewebsites.net/?q=e827f4d0-357c-4bbf-b6c5-0e8b06cceb22"",
                        ""title"": ""See on map"",
                        ""webview_height_ratio"": ""compact"",
                        ""messenger_extensions"": true
                    }
                    ]
                }
                }
            }");

            await connector.Conversations.ReplyToActivityAsync(message);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}