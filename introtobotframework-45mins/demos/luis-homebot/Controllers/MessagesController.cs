using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using LuisDemo.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LuisDemo
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                await SendTypingActivity(activity);
                await Conversation.SendAsync(activity, () => new HomeAutomationDialog());
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }


        private static async Task SendTypingActivity(Activity activity)
        {
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            var isTypingReply = activity.CreateReply();
            isTypingReply.Type = ActivityTypes.Typing;
            await connector.Conversations.ReplyToActivityAsync(isTypingReply);


            Thread.Sleep(1000); // simulate bot think time
        }
    }
}