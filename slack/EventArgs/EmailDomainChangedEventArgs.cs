using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/email_domain_changed


    public class EmailDomainChangedEventArgs
    {


        private String _email_domain;
        private String _event_ts;


        public EmailDomainChangedEventArgs(dynamic Data)
        {
            _email_domain = Data.email_domain;
            _event_ts = Data.event_ts;
        }


        public String email_domain
        {
            get
            {
                return _email_domain;
            }
        }


        public String event_ts
        {
            get
            {
                return _event_ts;
            }
        }


    }


}
