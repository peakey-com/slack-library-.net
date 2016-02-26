using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class MessageEventArgs
    {


        //https://api.slack.com/events/message


        private Slack.Client _client;

        private String _channel;
        private String _user;
        private String _text;
        private Slack.TimeStamp _ts;
        private String _team;


        public MessageEventArgs(Slack.Client Client, dynamic Data)
        {
            _client = Client;
            _channel = Data.channel;
            _user = Data.user;
            _text = Data.text;
            _ts = new Slack.TimeStamp((String) Data.ts);
            _team = Data.team;
        }


        public String channel
        {
            get
            {
                return _channel;
            }
        }


        public String user
        {
            get
            {
                return _user;   
            }
        }


        public String text
        {
            get
            {
                return _text;
            }
        }


        public Slack.TimeStamp ts
        {
            get
            {
                return _ts;
            }
        }


        public String team
        {
            get
            {
                return _team;
            }
        }


        public RTM.channel ChannelInfo
        {
            get
            {
                foreach (RTM.channel channel in _client.MetaData.channels)
                {
                    if (channel.id == _channel)
                    {
                        return channel;
                    }
                }
                return null;
            }
        }


        public RTM.ims IMSInfo
        {
            get
            {
                foreach (RTM.ims ims in _client.MetaData.ims)
                {
                    if (ims.id == _channel)
                    {
                        return ims;
                    }
                }
                return null;
            }
        }


        public RTM.user UserInfo
        {
            get
            {
                foreach (RTM.user user in _client.MetaData.users)
                {
                    if (user.id == _user)
                    {
                        return user;
                    }
                }
                return null;
            }
        }


    }


}
