using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack
{


    //https://api.slack.com/events/file_comment_edited


    public class FileCommentEditedEventArgs
    {


        private Slack.FileObject _file;
        private String _comment;


        public FileCommentEditedEventArgs(dynamic Data)
        {
            _file = new FileObject(Data.file);
            _comment = Data.comment;
        }


        public Slack.FileObject file
        {
            get
            {
                return _file;
            }
        }


        public String comment
        { 
            get
            {
                return _comment;
            }
        }


    }


}
