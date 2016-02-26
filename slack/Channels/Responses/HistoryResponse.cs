using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack.Channels
{


    //https://api.slack.com/methods/channels.history


    public class HistoryResponse
    {


        private Slack.Client _client;
        private Slack.Channels.HistoryRequestArgs _args;
        private Slack.TimeStamp _latest;
        private List<Slack.Messages.IMessage> _messages;
        private Boolean _hasMore;


        public HistoryResponse(Slack.Client Client, Slack.Channels.HistoryRequestArgs args, dynamic Response)
        {
            _client = Client;
            _args = args;
            _latest = new Slack.TimeStamp(Utility.TryGetProperty(Response, "latest", 0).ToString());
            _hasMore = Utility.TryGetProperty(Response, "has_more", false);
            _messages = new List<Messages.IMessage>();
            String strType;
            foreach (dynamic message in Response.messages)
            {
                strType = message.type;
                switch (strType)
                {
                    case "message":
                        _messages.Add(new Slack.Messages.Text(_client, message));
                        break;
                    default:
                        _messages.Add(new Slack.Messages.Unknown(message));
                        break;
                }
            }
        }


        public HistoryResponse NextPage()
        {
            if (!_hasMore)
            {
                return this;
            }
            _args.latest = _messages[_messages.Count - 1].ts;
            return _client.Channels.History(_args);
        }


        public Slack.TimeStamp latest
        {
            get
            {
                return _latest;
            }
        }


        public List<Slack.Messages.IMessage> Messages
        {
            get
            {
                return _messages;
            }
        }


        public Boolean HasMore
        {
            get
            {
                return _hasMore;
            }
        }


    }


}
