using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack.Channels
{


    //https://api.slack.com/methods/channels.list


    public class ListResponse
    {


        private List<RTM.channel> _channels;


        public ListResponse(Slack.RTM.MetaData MetaData, dynamic Response)
        {
            _channels = new List<RTM.channel>();
            if (Utility.HasProperty(Response, "channels"))
            {
                RTM.channel rtmChannel;
                foreach (dynamic channel in Response.channels)
                {
                    rtmChannel = new RTM.channel(MetaData);
                    rtmChannel.id = Utility.TryGetProperty(channel, "id");
                    rtmChannel.name = Utility.TryGetProperty(channel, "name");
                    rtmChannel.created = new Slack.TimeStamp(Utility.TryGetProperty(channel, "created", 0));
                    rtmChannel.creator = Utility.TryGetProperty(channel, "creator");
                    rtmChannel.is_archived = Utility.TryGetProperty(channel, "is_archived", false);
                    rtmChannel.is_member = Utility.TryGetProperty(channel, "is_member", false);
                    _channels.Add(rtmChannel);
                }
            }
        }


        public List<RTM.channel> channels
        {
            get
            {
                return _channels;
            }
        }


    }


}
