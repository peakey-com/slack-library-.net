using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/team_rename


    public class TeamRenameEventArgs
    {


        private String _name;


        public TeamRenameEventArgs(dynamic Data)
        {
            _name = Data.name;
        }


        public String name
        {
            get
            {
                return _name;
            }
        }


    }


}
