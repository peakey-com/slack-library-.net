using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/team_pref_change


    public class TeamPrefChangeEventArgs
    {


        private String _name;
        private String _value;


        public TeamPrefChangeEventArgs(dynamic Data)
        {
            _name = Data.name;
            _value = Data.value;
        }


        public String name
        {
            get
            {
                return _name;
            }
        }


        public String value
        {
            get
            {
                return _value;
            }
        }


    }


}