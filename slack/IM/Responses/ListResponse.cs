using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack
{
    public partial class IM
    {


        public class ListResponse
        {

            //https://api.slack.com/methods/im.list


            public class IM
            {


                private Slack.Client _client;

                public String id;
                public Boolean is_im;
                public String user;
                public Slack.TimeStamp created;
                public Boolean is_user_deleted;


                public IM(Slack.Client client)
                {
                    _client = client;
                }


                public RTM.user UserInfo
                {
                    get
                    {
                        if (_client.MetaData == null)
                        {
                            return null;
                        }
                        foreach (RTM.user rtmUser in _client.MetaData.users)
                        {
                            if (rtmUser.id == user)
                            {
                                return rtmUser;
                            }
                        }
                        return null;
                    }
                }

            
            }


            public List<IM> ims;


            public ListResponse(Slack.Client client, dynamic Response)
            {
                ims = new List<IM>();
                if (!Utility.HasProperty(Response, "ims"))
                {
                    return;
                }
                IM objIM;
                foreach (dynamic im in Response.ims)
                {
                    objIM = new IM(client);
                    objIM.id = Utility.TryGetProperty(im, "id");
                    objIM.is_im = Utility.TryGetProperty(im, "is_im", false);
                    objIM.user = Utility.TryGetProperty(im, "user");
                    objIM.created = new TimeStamp(Utility.TryGetProperty(im, "created"));
                    objIM.is_user_deleted = Utility.TryGetProperty(im, "is_user_deleted", false);
                    ims.Add(objIM);
                }
            }


        }


    }
}