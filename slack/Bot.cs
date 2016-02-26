using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/bot_added


    public class Bot
    {


        private String _id;
        private String _name;
        private Icons _icons;


        public Bot(dynamic Data)
        {
            _id = Data.id;
            _name = Data.name;
            _icons = new Icons(Data.icons);
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


        public Icons icons
        {
            get
            {
                return _icons;
            }
        }


    }


}
