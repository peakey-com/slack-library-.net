using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/subteam_created


    public class SubTeamCreatedEventArgs
    {


        private Slack.SubTeam _subteam;


        public SubTeamCreatedEventArgs(dynamic Data)
        {
            _subteam = new SubTeam(Data.subteam);
        }


        public Slack.SubTeam subteam
        {
            get
            {
                return _subteam;
            }
        }


    }


}
