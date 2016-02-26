using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/events/file_change


    public class FileChangeEventArgs
    {


        private Slack.FileObject _file;


        public FileChangeEventArgs(dynamic Data)
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
