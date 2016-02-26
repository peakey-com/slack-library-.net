using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/methods/auth.test


    public class AuthTestResponse
    {


        private String _team;
        private String _team_id;
        private String _url;
        private String _user;
        private String _user_id;


        public AuthTestResponse(dynamic Response)
        {
            _team = Response.team;
            _team_id = Response.team_id;
            _url = Response.url;
            _user = Response.user;
            _user_id = Response.user_id;
        }


        public String team
        {
            get
            {
                return _team;
            }
        }


        public String team_id
        {
            get
            {
                return _team_id;
            }
        }


        public String url
        {
            get
            {
                return _url;
            }
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


        public String user_id
        {
            get
            {
                return _user_id;
            }
        }


    }


}
