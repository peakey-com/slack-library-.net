using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public partial class DND
    {


        private Slack.Client _client;


        public DND(Slack.Client Client)
        {
            _client = Client;
        }


        public Slack.DoNotDisturbStatus Info(String user)
        {
            //https://api.slack.com/methods/dnd.info
            dynamic Response;
            try
            {
                String strURL = "https://slack.com/api/dnd.info?token=" + _client.APIKey;
                if ( (user != null) && (user.Trim().Length > 0) )
                {
                    strURL += "&user=" + System.Web.HttpUtility.UrlEncode(user);
                }
                String strResponse = _client.APIRequest(strURL);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get user dnd info.", ex);
            }
            _client.CheckForError(Response);
            return new DoNotDisturbStatus(Response);
        }


        public Slack.DoNotDisturbStatus SetSnooze(Int32 minutes)
        {
            //https://api.slack.com/methods/dnd.setSnooze
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest(
                    "https://slack.com/api/dnd.setSnooze?token=" + _client.APIKey +
                    "&num_minutes=" + System.Web.HttpUtility.UrlEncode(minutes.ToString())
                    );
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get user dnd info.", ex);
            }
            _client.CheckForError(Response);
            return new DoNotDisturbStatus(Response);
        }


        public Slack.DoNotDisturbStatus EndSnooze()
        {
            //https://api.slack.com/methods/dnd.endSnooze
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/dnd.endSnooze?token=" + _client.APIKey);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get user dnd info.", ex);
            }
            _client.CheckForError(Response);
            return new DoNotDisturbStatus(Response);
        }


        public Boolean EndDND()
        {
            //https://api.slack.com/methods/dnd.endDnd
            dynamic Response;
            try
            {
                String strResponse = _client.APIRequest("https://slack.com/api/dnd.endDnd?token=" + _client.APIKey);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get user dnd info.", ex);
            }
            _client.CheckForError(Response);
            return Utility.TryGetProperty(Response, "ok", false);
        }


    }


}
