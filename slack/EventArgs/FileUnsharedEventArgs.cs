using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/file_unshared


    public class FileUnsharedEventArgs
    {


        private Slack.FileObject _file;


        public FileUnsharedEventArgs(dynamic Data)
        {
            _file = new FileObject(Data.file);
        }

        public Slack.FileObject file
        {
            get
            {
                return _file;
            }
        }

    
    }


}
