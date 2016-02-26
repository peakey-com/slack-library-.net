using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/user_change


    public class UserChangeEventArgs
    {


        private dynamic _user;


        public UserChangeEventArgs(dynamic Data)
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
