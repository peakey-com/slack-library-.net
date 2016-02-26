using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/file_deleted


    public class FileDeletedEventArgs
    {


        private String _file_id;
        private String _event_ts;


        public FileDeletedEventArgs(dynamic Data)
        {
            _file_id = Data.file_id;
            _event_ts = Data.event_ts;
        }


        public String file_id
        {
            get
            {
                return _file_id;
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
