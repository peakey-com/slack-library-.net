using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public partial class IM
    {


        private Slack.Client _client;


        public IM(Slack.Client Client)
        {
            _client = Client;
        }


        public OpenResponse Open(String strUserID)
        {
            //https://api.slack.com/methods/im.open
            dynamic Response;
            try
            {
                String strURL =
                    "https://slack.com/api/im.open?token=" + _client.APIKey +
                    "&user=" + System.Web.HttpUtility.UrlEncode(strUserID);
                String strResponse = _client.APIRequest(strURL);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not list channels.", ex);
            }
            _client.CheckForError(Response);
            return new Slack.IM.OpenResponse(Response);
        }


        public ListResponse List()
        {
            //https://api.slack.com/methods/im.list
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/im.list?token=" + _client.APIKey);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not list channels.", ex);
            }
            _client.CheckForError(Response);
            return new Slack.IM.ListResponse(_client, Response);
        }


        public Boolean Close(String channel)
        {
            //https://api.slack.com/methods/im.close
            dynamic Response;
            try
            {
                String strURL =
                    "https://slack.com/api/im.close?token=" + _client.APIKey +
                    "&channel=" + System.Web.HttpUtility.UrlEncode(channel);
                String strResponse = _client.APIRequest(strURL);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not list channels.", ex);
            }
            _client.CheckForError(Response);
            return Utility.TryGetProperty(Response, "ok", false);
        }


    }



}
