using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class FileSharedEventArgs
    {


        //https://api.slack.com/events/file_shared


        public Slack.FileObject _file;


        public FileSharedEventArgs(dynamic Data)
        {
            _file = new Slack.FileObject(Data.file);
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
