using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class ChannelCreatedEventArgs
    {


        //https://api.slack.com/events/channel_created


        private String _id;
        private String _name;
        private Int32 _created;
        private String _creator;


        public ChannelCreatedEventArgs(dynamic Data)
        {
            _created = Data.created;
            _creator = Data.creator;
            _id = Data.id;
            _name = Data.name;
        }


        public String id
        {
            get
            {
                return _id;
            }
        }


        public String name
        {
            get
            {
                return _name;
            }
        }


        public Int32 created
        {
            get
            {
                return _created;
            }
        }


        public String creator
        {
            get
            {
                return _creator;
            }
        }
    
    
    }


}
