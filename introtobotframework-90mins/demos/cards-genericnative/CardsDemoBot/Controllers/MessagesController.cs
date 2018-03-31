using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CardsDemoBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace CardsDemoBot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
                await Conversation.SendAsync(activity, () => new CardsDemoDialog());

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}