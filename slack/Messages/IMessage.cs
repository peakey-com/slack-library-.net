using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Messages
{

    public interface IMessage
    {


        String Type
        {
            get;
        }


        Slack.TimeStamp ts
        {
            get;
        }


    }


}
