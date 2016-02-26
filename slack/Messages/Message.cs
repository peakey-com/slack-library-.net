using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Messages
{


    public class Message : IMessage
    {


        public class Edited
        {


            private String _user;
            private String _ts;


            public Edited(dynamic Data)
            {
                _ts = Data.ts;
                _user = Data.user;
            }


            public String user
            {
                get
                {
                    return _user;   
                }
            }


            public String ts
            {
                get
                {
                    return _ts;
                }
            }


        }


        private String _user;
        private String _text;
        private Edited _edited;
        private Slack.TimeStamp _ts;


        public Message(dynamic Data)
        {
            _edited = new Edited(Data.edited);
            _text = Data.message.text;
            _ts = new Slack.TimeStamp( (String)Data.message.ts);
            _user = Data.message.user;
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


        public Edited edited
        {
            get
            {
                return _edited;
            }
        }


        public String Type
        {
            get
            {
                return "message";
            }
        }


        public Slack.TimeStamp ts
        {
            get
            {
                return _ts;
            }
        }

    
    }


}
