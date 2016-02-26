using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack
{
    public partial class Chat
    {


        //https://api.slack.com/methods/chat.postMessage


        public class PostMessageResponse
        {


            public String channel;
            public Slack.TimeStamp ts;
            public Slack.Messages.Text message;


            public PostMessageResponse(Slack.Client client, dynamic Response)
            {
                channel = Utility.TryGetProperty(Response, "channel");
                ts = new Slack.TimeStamp(Utility.TryGetProperty(Response, "ts"));
                message = new Slack.Messages.Text(client, Response.message);
            }


        }


    }
}