using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/bot_added


    public class BotAddedEventArgs
    {


        private Bot _bot;


        public BotAddedEventArgs(dynamic Data)
        {
            _bot = new Bot(Data);
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
