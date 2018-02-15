using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    //https://api.slack.com/types/file


    public class FileObject
    {

        public class reaction
        {


            private String _name;
            private Int32 _count;
            private String[] _users;


            public reaction(dynamic Data)
            {
                _name = Data.name;
                _count = Data.count;
                _users = Data.users;
            }


            public String name
            {
                get
                {
                    return _name;
                }
            }


            public Int32 count
            {
                get
                {
                    return _count;
                }
            }


            public String[] users
            {
                get
                {
                    return _users;
                }
            }


        }


        public String _id;
        public Int32 _created;
        public Int32 _timestamp;

        public String _name;
        public String _title;
        public String _mimetype;
        public String _filetype;
        public String _pretty_type;
        public String _user;

        public String _mode;
        public Boolean _editable;
        public Boolean _is_external;
        public String _external_type;

        public Int32 _size;

        public String _url_private;
        public String _url_private_download;

        public String _thumb_64;
        public String _thumb_80;
        public String _thumb_360;
        public String _thumb_360_gif;
        public Int32 _thumb_360_w;
        public Int32 _thumb_360_h;

        public String _permalink;
        public String _edit_link;
        public String _preview;
        public String _preview_highlight;
        public Int32 _lines;
        public Int32 _lines_more;

        public Boolean _is_public;
        public Boolean _public_url_shared;
        public String[] _channels;
        public String[] _groups;
        public String[] _ims;
        public String _initial_comment;
        public Int32 _num_stars;
        public Boolean _is_starred;
        public String[] _pinned_to;
        public reaction[] _reactions;


        public FileObject(dynamic Data)
        {
            _id = Data.id;
            _created = Utility.TryGetProperty(Data, "created", 0);
            _timestamp = Utility.TryGetProperty(Data, "timestamp", 0);
            _name = Data.name;
            _title = Data.title;
            _mimetype = Data.mimetype;
            _filetype = Data.filetype;
            _pretty_type = Data.pretty_type;
            _user = Data.user;
            _mode = Data.mode;
            if (!Boolean.TryParse(Data.editable, out _editable))
            {
                _editable = false;
            }
            _is_external = Utility.TryGetProperty(Data, "is_external", false);
            _external_type = Data.external_type;
            _size = Data.size;
            _url_private = Data.url_private;
            _url_private_download = Data.url_private_download;
            _thumb_64 = Data.thumb_64;
            _thumb_80 = Data.thumb_80;
            _thumb_360 = Data.thumb_360;
            _thumb_360_gif = Data.thumb_360_gif;
            _thumb_360_w = Data.thumb_360_w;
            _thumb_360_h = Data.thumb_360_h;
            _permalink = Data.permalink;
            _edit_link = Data.edit_link;
            _preview = Data.preview;
            _preview_highlight = Data.preview_highlight;
            _lines = Data.lines;
            _lines_more = Data.lines_more;
            _is_public = Data.is_public;
            _public_url_shared = Data.public_url_shared;
            _channels = Data.channels;
            _groups = Data.groups;
            _ims = Data.ims;
            _initial_comment = Data.initial_comment;
            _num_stars = Data.num_stars;
            _is_starred = Data.is_starred;
            _pinned_to = Data.pinned_to;
            _reactions = new reaction[Data.reactions.length];
            for (Int32 intCounter = 0; intCounter < Data.reactions.length; intCounter++ )
            {
                _reactions[intCounter] = new reaction(Data.reactions[intCounter]);
            }
        }


        public String id
        {
            get
            {
                return _id;
            }
        }


        public Int32 created
        {
            get
            {
                return _created;
            }
        }


        public Int32 timestamp
        {
            get
            {
                return _timestamp;
            }
        }


        public String name
        {
            get
            {
                return _name;
            }
        }


        public String title
        {
            get
            {
                return _title;
            }
        }


        public String mimetype
        {
            get
            {
                return _mimetype;
            }
        }


        public String filetype
        {
            get
            {
                return _filetype;
            }
        }


        public String pretty_type
        {
            get
            {
                return _pretty_type;
            }
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


        public String mode
        {
            get
            {
                return _mode;
            }
        }


        public Boolean editable
        {
            get
            {
                return _editable;
            }
        }


        public Boolean is_external
        {
            get
            {
                return _is_external;
            }
        }


        public String external_type
        {
            get
            {
                return _external_type;
            }
        }


        public Int32 size
        {
            get
            {
                return _size;
            }
        }


        public String url_private
        {
            get
            {
                return _url_private;
            }
        }


        public String url_private_download
        {
            get
            {
                return _url_private_download;
            }
        }


        public String thumb_64
        {
            get
            {
                return _thumb_64;
            }
        }


        public String thumb_80
        {
            get
            {
                return _thumb_80;
            }
        }


        public String thumb_360
        {
            get
            {
                return _thumb_360;
            }
        }


        public String thumb_360_gif
        {
            get
            {
                return _thumb_360_gif;
            }
        }


        public Int32 thumb_360_w
        { 
            get
            {
                return _thumb_360_w;
            }
        }


        public Int32 thumb_360_h
        {
            get
            {
                return _thumb_360_h;
            }
        }


        public String permalink
        {
            get
            {
                return _permalink;
            }
        }


        public String edit_link
        {
            get
            {
                return _edit_link;
            }
        }


        public String preview
        {
            get
            {
                return _preview;
            }
        }


        public String preview_highlight
        {
            get
            {
                return _preview_highlight;
            }
        }


        public Int32 lines
        {
            get
            {
                return _lines;
            }
        }


        public Int32 lines_more
        {
            get
            {
                return _lines_more;
            }
        }


        public Boolean is_public
        {
            get
            {
                return _is_public;
            }
        }


        public Boolean public_url_shared
        {
            get
            {
                return _public_url_shared;
            }
        }


        public String[] channels
        {
            get
            {
                return _channels;
            }
        }


        public String[] groups
        {
            get
            {
                return _groups;
            }
        }


        public String[] ims
        {
            get
            {
                return _ims;
            }
        }


        public String initial_comment
        {
            get
            {
                return _initial_comment;
            }
        }


        public Int32 num_stars
        {
            get
            {
                return _num_stars;
            }
        }


        public Boolean is_starred
        {
            get
            {
                return _is_starred;
            }
        }


        public String[] pinned_to
        {
            get
            {
                return _pinned_to;
            }
        }


        public reaction[] reactions
        {
            get
            {
                return _reactions;
            }
        }


    }


}
