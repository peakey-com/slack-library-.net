using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/subteam_self_added


    public class SubTeamSelfAddedEventArgs
    {


        private String _subteam_id;


        public SubTeamSelfAddedEventArgs(dynamic Data)
        {
            _subteam_id = Data.subteam_id;
        }


        public String subteam_id
        {
            get
            {
                return _subteam_id;
            }
        }


    }


}
