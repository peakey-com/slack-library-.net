#Slack Client Library C# (.net)
This repo contains the slack client library "slack" and a "test" project that demonstrates usage of the slack client library.
The library supports many of the slack events and has defined most slack exceptions.


The test app requires a single parameter  by accepting a single parameter which is your slack API key


###To create a new slack client
>client = new Slack.Client("YOUR SLACK API KEY HERE");

###Examples of subscribing to slack the events that may interest you
###More information and examples are available in the test app
>client.Hello += new Slack.Client.HelloEventHandler(client_Hello);

>client.DataReceived += new Slack.Client.DataReceivedEventHandler(client_DataReceived);

>client.PresenceChanged += new Slack.Client.PresenceChangedEventHandler(client_PresenceChanged);

>client.UserTyping += new Slack.Client.UserTypingEventHandler(client_UserTyping);

>client.Message += new Slack.Client.MessageEventHandler(client_Message);

>client.MesssageEdit += new Slack.Client.MessageEditEventHandler(client_MessageEdit);

>client.DoNotDisturbUpdatedUser += new Slack.Client.DoNotDistrubUpdatedUserEventHandler(client_DoNotDisturbUpdatedUser);

###connect to the slack service
>client.Connect();


###disconnect from slack service
>client.Disconnect();


