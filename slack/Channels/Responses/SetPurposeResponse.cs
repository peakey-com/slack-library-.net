using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack.Channels
{


    //https://api.slack.com/methods/channels.setPurpose


    public class SetPurposeResponse
    {


        public String purpose;


        public SetPurposeResponse(dynamic Response)
        {
            purpose = Utility.TryGetProperty(Response, "purpose");
        }


    }


}
