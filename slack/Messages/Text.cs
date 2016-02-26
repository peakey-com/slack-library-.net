using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Messages
{


    public class Text : IMessage
    {


        private Slack.Client _client;
        private String _type;
        private Slack.TimeStamp _ts;

        private String _user;
        private String _text;


        public Text(Slack.Client Client, dynamic Data)
        {
            _client = Client;
            _type = Utility.TryGetProperty(Data, "type");
            _ts = new Slack.TimeStamp(Utility.TryGetProperty(Data, "ts", 0));
            _user = Utility.TryGetProperty(Data, "user");
            if (_user == "")
            {
                _user = Utility.TryGetProperty(Data, "username");
            }
            _text = Utility.TryGetProperty(Data, "text");
        }


        public String Type
        {
            get
            {
                return _type;
            }
        }


        public Slack.TimeStamp ts
        {
            get
            {
                return _ts;
            }
        }


        public String text
        {
            get
            {
                return _text;
            }
        }


        public String User
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
