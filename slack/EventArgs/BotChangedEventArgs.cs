using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/bot_changed


    public class BotChangedEventArgs
    {


        private Bot _bot;


        public BotChangedEventArgs(dynamic Data)
        {
            _bot = new Bot(Data.bot);
        }


        public Bot bot
        {
            get
            {
                return _bot;
            }
        }


    }


}
