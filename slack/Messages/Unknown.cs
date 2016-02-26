using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Messages
{


    public class Unknown : IMessage
    {


        private String _type;
        private Slack.TimeStamp _ts;


        public Unknown(dynamic Data)
        {
            _type = Data.type;
            _ts = new Slack.TimeStamp((String)Data.message.ts);
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


    }


}
