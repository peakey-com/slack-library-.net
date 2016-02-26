using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/team_profile_change


    public class TeamProfileChangeEventArgs
    {


        private dynamic _profile;


        public TeamProfileChangeEventArgs(dynamic Data)
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
