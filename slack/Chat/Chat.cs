using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public partial class Chat
    {
        

        private Slack.Client _client;


        public Chat(Slack.Client Client)
        {
            _client = Client;
        }


        public Boolean Delete(String channel, Slack.TimeStamp ts)
        {
            //https://api.slack.com/methods/chat.delete
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest(
                    "https://slack.com/api/chat.delete?token=" + _client.APIKey +
                    "&channel=" + System.Web.HttpUtility.UrlEncode(channel) +
                    "&ts=" + System.Web.HttpUtility.UrlEncode(ts.ToString()));
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not delte message.", ex);
            }
            _client.CheckForError(Response);
            return Response.ok;
        }


        public PostMessageResponse PostMessage(PostMessageArguments args)
        {
            //https://api.slack.com/methods/chat.postMessage
            String strURL = "";
            try
            {
                strURL =
                    "https://slack.com/api/chat.postMessage?token=" + _client.APIKey +
                    "&channel=" + System.Web.HttpUtility.UrlEncode(args.channel) +
                    "&text=" + System.Web.HttpUtility.UrlEncode(args.text) +
                    "&as_user=" + args.as_user.ToString() +
                    "&parse=" + System.Web.HttpUtility.UrlEncode(args.parse) +
                    "&link_names=" + System.Web.HttpUtility.UrlEncode(args.link_names.ToString()) +
                    "&unfurl_links=" + args.unfurl_links.ToString() +
                    "&unfurl_media=" + args.unfurl_media.ToString();
                if (args.username.Trim().Length > 0)
                {
                    strURL += "&username=" + System.Web.HttpUtility.UrlEncode(args.username);
                }
                if (args.icon_url.Trim().Length > 0)
                {
                    strURL += "&icon_url=" + System.Web.HttpUtility.UrlEncode(args.icon_url);
                }
                if (args.icon_emoji.Trim().Length > 0)
                {
                    strURL += "&icon_emoji=" + System.Web.HttpUtility.UrlEncode(args.icon_emoji);
                }
                if (args.attachments != null)
                {
                    strURL += "&attachments=[{";
                    for (Int32 intAttachment = 0; intAttachment < args.attachments.Length; intAttachment++ )
                    {
                        strURL +=
                            "\"" + System.Web.HttpUtility.UrlEncode(args.attachments[intAttachment].type) + "\": " +
                            "\"" + System.Web.HttpUtility.UrlEncode(args.attachments[intAttachment].value) + "\"";
                        if (intAttachment < (args.attachments.Length - 1))
                        {
                            strURL += ", ";
                        }
                    }
                    strURL += "}]";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not post chat message.", ex);
            }
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest(strURL);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not post chat message.", ex);
            }
            _client.CheckForError(Response);
            return new Slack.Chat.PostMessageResponse(_client, Response);
        }


        public UpdateMessageResponse Update(UpdateMessageArguments args)
        {
            //https://api.slack.com/methods/chat.update
            String strURL = "";
            try
            {
                strURL =
                    "https://slack.com/api/chat.update?token=" + _client.APIKey +
                    "&channel=" + System.Web.HttpUtility.UrlEncode(args.channel) +
                    "&ts=" + System.Web.HttpUtility.UrlEncode(args.ts.ToString()) +
                    "&text=" + System.Web.HttpUtility.UrlEncode(args.text) +
                    "&parse=" + System.Web.HttpUtility.UrlEncode(args.parse) +
                    "&link_names=" + System.Web.HttpUtility.UrlEncode(args.link_names.ToString());
                if (args.attachments != null)
                {
                    strURL += "&attachments=[{";
                    for (Int32 intAttachment = 0; intAttachment < args.attachments.Length; intAttachment++)
                    {
                        strURL +=
                            "\"" + System.Web.HttpUtility.UrlEncode(args.attachments[intAttachment].type) + "\": " +
                            "\"" + System.Web.HttpUtility.UrlEncode(args.attachments[intAttachment].value) + "\"";
                        if (intAttachment < (args.attachments.Length - 1))
                        {
                            strURL += ", ";
                        }
                    }
                    strURL += "}]";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not update chat message.", ex);
            }
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest(strURL);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not update chat message.", ex);
            }
            _client.CheckForError(Response);
            return new UpdateMessageResponse(Response);
        }


    }



}
