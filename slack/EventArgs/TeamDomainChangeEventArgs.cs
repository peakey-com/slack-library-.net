using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/team_domain_change


    public class TeamDomainChangeEventArgs
    {


        private String _url;
        private String _domain;


        public TeamDomainChangeEventArgs(dynamic Data)
        {
            _url = Data.url;
            _domain = Data.domain;
        }


        public String url
        {
            get
            {
                return _url;
            }
        }


        public String domain
        {
            get
            {
                return _domain;
            }
        }


    }


}
