using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class DoNotDisturbUpdatedEventArgs
    {


        //https://api.slack.com/events/dnd_updated


        private String _user;
        private DoNotDisturbStatus _dnd_status;


        public DoNotDisturbUpdatedEventArgs(dynamic Data)
        {
            _dnd_status = new DoNotDisturbStatus(Data.dnd_status);
            _user = Data.user;
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


        public DoNotDisturbStatus dnd_status
        {
            get
            {
                return _dnd_status;
            }
        }


    }


}
