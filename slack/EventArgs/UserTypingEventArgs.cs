using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class UserTypingEventArgs
    {


        //https://api.slack.com/events/user_typing


        private Slack.Client _client;

        private String _channel;
        private String _user;


        public UserTypingEventArgs(Slack.Client Client, dynamic Data)
        {
            _client = Client;
            _channel = Data.channel;
            _user = Data.user;
        }


        public String channel
        {
            get
            {
                return _channel;
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


        public String user
        {
            get
            {
                return _user;
            }
        }


        public RTM.user UserInfo
        {
            get
            {
                foreach(RTM.user user in  _client.MetaData.users)
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
