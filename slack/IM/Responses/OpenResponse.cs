using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack
{
    public partial class IM
    {


        public class OpenResponse
        {

            //https://api.slack.com/methods/im.open

            private String _channelID = "";


            public OpenResponse(dynamic Response)
            {
                if (!Utility.HasProperty(Response, "channel"))
                {
                    return;
                }
                _channelID = Utility.TryGetProperty(Response.channel, "id");
            }


            public String ChannelID
            {
                get
                {
                    return _channelID;
                }
            }


        }


    }
}