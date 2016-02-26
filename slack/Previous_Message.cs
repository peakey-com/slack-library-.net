using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class Previous_Message
    {


        private String _type;
        private String _user;
        private String _text;
        private String _ts;


        public Previous_Message(dynamic Data)
        {
            _text = Data.text;
            _ts = Data.ts;
            _type = Data.type;
            _user = Data.user;
        }


        public String type
        {
            get
            {
                return _type;
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


        public String ts
        {
            get
            {
                return _ts;
            }
        }

    
    }


}
