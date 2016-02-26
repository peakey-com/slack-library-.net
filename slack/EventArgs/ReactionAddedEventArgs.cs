using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/reaction_added


    public class ReactionAddedEventArgs
    {


        private String _user;
        private String _name;
        private dynamic _item;
        private String _event_ts;


        public ReactionAddedEventArgs(dynamic Data)
        {
            _user = Data.user;
            _name = Data.name;
            _item = Data.item;
            _event_ts = Data.event_ts;
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


        public String name
        {
            get
            {
                return _name;
            }
        }


        public dynamic item
        {
            get
            {
                return _item;
            }
        }


        public String event_ts
        {
            get
            {
                return _event_ts;
            }
        }


    }


}
