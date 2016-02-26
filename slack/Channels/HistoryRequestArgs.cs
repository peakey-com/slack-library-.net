using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack.Channels
{


    public class HistoryRequestArgs
    {


        public String channel;
        public Slack.TimeStamp latest = new Slack.TimeStamp(DateTime.Now.ToUniversalTime());
        public Slack.TimeStamp oldest = new Slack.TimeStamp(Slack.TimeStamp.MinValue);
        public Boolean inclusive = false;
        public Boolean unreads = false;


        private Int32 _count = 100;


        public HistoryRequestArgs(String channel)
        {
            this.channel = channel;
        }


        public HistoryRequestArgs(String channel, DateTime latest, DateTime oldest, Boolean inclusive, Int32 count, Boolean unreads)
        {
            this.channel = channel;
            this.latest.Date = latest;
            this.oldest.Date = oldest;
            this.inclusive = inclusive;
            this.count = count;
            this.unreads = unreads;
        }


        public HistoryRequestArgs(String channel, Slack.TimeStamp latest, Slack.TimeStamp oldest, Boolean inclusive, Int32 count, Boolean unreads)
        {
            this.channel = channel;
            this.latest = latest;
            this.oldest = oldest;
            this.inclusive = inclusive;
            this.count = count;
            this.unreads = unreads;
        }


        public Int32 count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                if (count > 100)
                {
                    count = 100;
                }
                else if (count < 1)
                {
                    count = 1;
                }
            }
        }



    }


}
