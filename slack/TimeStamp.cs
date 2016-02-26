using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class TimeStamp
    {


        public static DateTime MinValue = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);


        private DateTime date;
        private Int32 intOrder;


        public TimeStamp(DateTime Date)
        {

            if (Date < MinValue)
            {
                Date = MinValue;
            }
            this.date = Date;
        }


        public TimeStamp(Double UnixTimeStamp)
        {
            date = MinValue.AddSeconds(UnixTimeStamp).ToLocalTime();
        }


        public TimeStamp(String TimeStamp)
        {
            String strTime = TimeStamp;
            intOrder = 0;
            if (TimeStamp.Contains("."))
            {
                strTime = TimeStamp.Substring(0, TimeStamp.IndexOf("."));
                String strOrder = TimeStamp.Substring(TimeStamp.IndexOf(".") + 1);
                Int32.TryParse(strOrder, out intOrder);
            }
            Double dblTimeStamp;
            Double.TryParse(strTime, out dblTimeStamp);
            date = MinValue.AddSeconds(dblTimeStamp).ToLocalTime();
        }
        

        public override String ToString()
        {
            DateTime dtUTC = date.ToUniversalTime();
            Double dblSeconds = dtUTC.Subtract(MinValue).TotalSeconds;
            if (intOrder > 0)
            {
                return dblSeconds.ToString() + "." + intOrder.ToString();
            }
            return dblSeconds.ToString();
        }


        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }


        public Int32 Order
        {
            get
            {
                return intOrder;
            }
        }


    }


}
