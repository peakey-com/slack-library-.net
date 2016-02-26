using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/emoji_changed


    public class EmojiChangedEventArgs
    {


        private String _event_ts;


        public EmojiChangedEventArgs(dynamic Data)
        {
            _event_ts = Data.event_ts;
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
