using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{
    public partial class RTM
    {


        public class ims
        {

            private MetaData _metaData;

            public Slack.TimeStamp created;
            public Boolean has_pins;
            public String id;
            public Boolean is_im;
            public Boolean is_open;
            public String last_read;
            public Int32 unread_count;
            public Int32 unread_count_display;
            public String user;


            public ims(MetaData MetaData)
            {
                _metaData = MetaData;
            }


            public Slack.TimeStamp CreatedDate
            {
                get
                {
                    return created;
                }
            }


            public RTM.user UserInfo
            {
                get
                {
                    foreach (RTM.user userItem in _metaData.users)
                    {
                        if (userItem.id == user)
                        {
                            return userItem;
                        }
                    }
                    return null;
                }
            }


        }


    }
}