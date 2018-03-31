using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FormFlowBot.Forms;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace FormFlowBot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity.Type == ActivityTypes.ConversationUpdate &&
                activity.MembersAdded.Any(m => m.Id == activity.Recipient.Id))
            {
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                var reply = activity.CreateReply($"So you're having a problem with the parking machine! :D");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity,
                    () => Chain.From(() => FormDialog.FromForm(ParkingEnquiryForm.BuildForm)));
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}