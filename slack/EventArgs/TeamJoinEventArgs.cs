using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/team_join


    public class TeamJoinEventArgs
    {


        private dynamic _user;


        public TeamJoinEventArgs(dynamic Data)
        {
            _user = Data.user;
        }


        public dynamic user
        {
            get
            {
                return _user;
            }
        }


    }


}
