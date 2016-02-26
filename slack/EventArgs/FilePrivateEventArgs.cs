using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/file_private


    public class FilePrivateEventArgs
    {


        private String _file;


        public FilePrivateEventArgs(dynamic Data)
        {
            _file = Data.file;
        }


        public String file
        {
            get
            {
                return _file;
            }
        }

    
    }


}
