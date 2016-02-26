using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/subteam_created


    public class SubTeam
    {


        private String _id;
        private String _team_id;
        private Boolean _is_usergroup;
        private String _name;
        private String _description;
        private String _handle;
        private Boolean _is_external;
        private Int32 _date_created;
        private Int32 _date_updated;
        private Int32 _date_delete;
        private String _auto_type;
        private String _created_by;
        private String _updated_by;
        private String _deleted_by;
        private Prefs _prefs;
        private String _user_count;


        public SubTeam(dynamic Data)
        {
            _id = Data.id;
            _team_id = Data.team_id;
            _is_usergroup = Data.is_usergroup;
            _name = Data.name;
            _description = Data.description;
            _handle = Data.handle;
            _is_external = Data.is_external;
            _date_created = Data.date_created;
            _date_updated = Data.date_updated;
            _date_delete = Data.date_delete;
            _auto_type = Data.auto_type;
            _created_by = Data.created_by;
            _updated_by = Data.updated_by;
            _deleted_by = Data.deleted_by;
            _prefs = Data.prefs;
            _user_count = Data.user_count;
        }


        public String id
        {
            get
            {
                return _id;
            }
        }


        public String team_id
        {
            get
            {
                return _team_id;
            }
        }


        public Boolean is_usergroup
        {
            get
            {
                return _is_usergroup;
            }
        }


        public String name
        {
            get
            {
                return _name;
            }
        }


        public String description
        {
            get
            {
                return _description;
            }
        }


        public String handle
        {
            get
            {
                return _handle;
            }
        }


        public Boolean is_external
        {
            get
            {
                return _is_external;
            }
        }


        public Int32 date_created
        {
            get
            {
                return _date_created;
            }
        }


        public Int32 date_updated
        {
            get
            {
                return _date_updated;
            }
        }


        public Int32 date_delete
        {
            get
            {
                return _date_delete;
            }
        }


        public String auto_type
        {
            get
            {
                return _auto_type;
            }
        }


        public String created_by
        {
            get
            {
                return _created_by;
            }
        }


        public String updated_by
        {
            get
            {
                return _updated_by;
            }
        }


        public String deleted_by
        {
            get
            {
                return _deleted_by;
            }
        }


        public Prefs prefs
        {
            get
            {
                return _prefs;
            }
        }


        public String user_count
        {
            get
            {
                return _user_count;
            }
        }


    }


}