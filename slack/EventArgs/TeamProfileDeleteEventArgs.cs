using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/team_profile_delete


    public class TeamProfileDeleteEventArgs
    {


        private dynamic _profile;


        public TeamProfileDeleteEventArgs(dynamic Data)
        {
            _profile = Data.profile;
        }


        public dynamic profile
        {
            get
            {
                return _profile;
            }
        }


    }


}
