using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


        //https://api.slack.com/events/file_created


    public class FileCreatedEventArgs
    {


        private Slack.FileObject _file;


        public FileCreatedEventArgs(dynamic Data)
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
