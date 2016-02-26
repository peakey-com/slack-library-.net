using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{
    public partial class RTM
    {


        public class channel
        {


            public struct TopicInfo
            {
                public String creator;
                public Int32 last_set;
                public String value;
            }


            private MetaData _metaData = null;

            public Slack.TimeStamp created;
            public String creator;
            public Boolean has_pins;
            public String id;
            public Boolean is_archived;
            public Boolean is_channel;
            public Boolean is_general;
            public Boolean is_member;
            public String name;
            public List<String> members;
            public TopicInfo topic;
            public TopicInfo purpose;
            public Slack.TimeStamp last_read;
            public Int32 unread_count;
            public Int32 unread_count_display;


            public channel()
            {
            }


            public channel(MetaData MetaData)
            {
                _metaData = MetaData;
            }


            public channel(dynamic data)
            {
                id = Utility.TryGetProperty(data, "id");
                name = Utility.TryGetProperty(data, "name");
                created = new Slack.TimeStamp(Utility.TryGetProperty(data, "created", 0));
                creator = Utility.TryGetProperty(data, "creator");
                is_archived = Utility.TryGetProperty(data, "is_archived", false);
                is_general = Utility.TryGetProperty(data, "is_general", false);
                is_member = Utility.TryGetProperty(data, "is_member", false);
                members = new List<String>();
                if (Utility.HasProperty(data, "members"))
                {
                    foreach (String strMember in data.members)
                    {
                        members.Add(strMember);
                    }
                }
                if (Utility.HasProperty(data, "topic"))
                {
                    topic.creator = Utility.TryGetProperty(data.topic, "creator");
                    topic.last_set = Utility.TryGetProperty(data.topic, "last_set", 0);
                    topic.value = Utility.TryGetProperty(data.topic, "value");
                }
                if (Utility.HasProperty(data, "purpose"))
                {
                    purpose.creator = Utility.TryGetProperty(data.purpose, "creator");
                    purpose.last_set = Utility.TryGetProperty(data.purpose, "last_set", 0);
                    purpose.value = Utility.TryGetProperty(data.purpose, "value");
                }
                last_read = new TimeStamp(Utility.TryGetProperty(data, "last_read", 0));
                unread_count = Utility.TryGetProperty(data, "unread_count", 0);
                unread_count_display = Utility.TryGetProperty(data, "unread_count_display", 0);
            }


            public RTM.user CreatorUserInfo
            {
                get
                {
                    if (_metaData == null)
                    {
                        return null;
                    }
                    foreach (RTM.user user in _metaData.users)
                    {
                        if (user.id == creator)
                        {
                            return user;
                        }
                    }
                    return null;
                }
            }


        }


    }
}