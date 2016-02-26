using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class GroupRenameEventArgs
    {


        //https://api.slack.com/events/group_rename


        private String _id;
        private String _name;
        private Int32 _created;


        public GroupRenameEventArgs(dynamic Data)
        {
            _created = Data.created;
            _id = Data.id;
            _name = Data.name;
        }


        public String id
        { 
            get
            {
                return _id;
            }
        }


        public String name
        {
            get
            {
                return _name;
            }
        }


        public Int32 created
        {
            get
            {
                return _created;
            }
        }


    }


}
