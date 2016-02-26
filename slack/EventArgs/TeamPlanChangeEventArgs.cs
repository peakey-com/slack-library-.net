using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/team_plan_change


    public class TeamPlanChangeEventArgs
    {


        private String _plan;


        public TeamPlanChangeEventArgs(dynamic Data)
        {
            _plan = Data.plan;
        }


        public String plan
        {
            get
            {
                return _plan;
            }
        }


    }


}
