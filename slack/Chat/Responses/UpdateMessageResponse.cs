using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack
{
    public partial class Chat
    {


        //https://api.slack.com/methods/chat.update


        public class UpdateMessageResponse
        {


            public String channel;
            public Slack.TimeStamp ts;
            public String text;


            public UpdateMessageResponse(dynamic Response)
            {
                channel = Utility.TryGetProperty(Response, "channel");
                ts = new Slack.TimeStamp(Utility.TryGetProperty(Response, "ts"));
                text = Utility.TryGetProperty(Response, "text");
            }


        }


    }
}