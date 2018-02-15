using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace test
{


	class Program
	{


		private static Slack.Client client;
		private static Int32 connectionFailures = 0;
		private static Boolean shutdown = false;


		//main entry point for application
		static void Main(string[] args)
		{
			//this app works by accepting a single parameter which is the slack API key
			if (args.Length == 0)
			{
				Console.WriteLine("You must pass your api token as the first command line parameter");
				return;
			}

			//this is used below to integrate slack with time tracker service
			TimeTracker_LoadAccounts();

			//create a new slack client
			client = new Slack.Client(args[0]);

			//subscribe to any of the events that may interest you
			client.ServiceConnected += new Slack.Client.ServiceConnectedEventHandler(client_ServiceConnected);
			client.ServiceConnectionFailed += new Slack.Client.ServiceConnectionFailedEventHandler(client_ServiceDisconnected_ServiceConnectionFailure);
			client.ServiceDisconnected += new Slack.Client.ServiceDisconnectedEventHandler(client_ServiceDisconnected_ServiceConnectionFailure);

			client.Hello += new Slack.Client.HelloEventHandler(client_Hello);
			client.DataReceived += new Slack.Client.DataReceivedEventHandler(client_DataReceived);
			client.PresenceChanged += new Slack.Client.PresenceChangedEventHandler(client_PresenceChanged);
			client.UserTyping += new Slack.Client.UserTypingEventHandler(client_UserTyping);
			client.Message += new Slack.Client.MessageEventHandler(client_Message);
			client.MesssageEdit += new Slack.Client.MessageEditEventHandler(client_MessageEdit);
			client.DoNotDisturbUpdatedUser += new Slack.Client.DoNotDistrubUpdatedUserEventHandler(client_DoNotDisturbUpdatedUser);


			//connect to the slack service
			client.Connect();

			//simply hold application open until user presses enter
			Console.WriteLine("Press Enter to Terminate.");
			Console.ReadLine();

			//disconnect from slack service
			shutdown = true;
			client.Disconnect();
		}


#region Slack Events


		private static void client_ServiceConnected()
		{
			connectionFailures = 0;
			Console.WriteLine("Connected to slack service.");
		}


		private static void client_ServiceDisconnected_ServiceConnectionFailure()
		{
			if (shutdown)
			{	//don't restart
				return;
			}
			try
			{
				connectionFailures++;
				if (connectionFailures < 13)
				{   //wait 5 seconds and try to reconnect
					System.Threading.Thread.Sleep(5 * 1000);
				}
				else
				{   //wait 1 minute and try to reconnect
					System.Threading.Thread.Sleep(60 * 1000);
				}
				Console.WriteLine("Attempting to reconnect to slack service. Attempt " + connectionFailures);
				client.Connect();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Could not handle service disconnected.\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }


		private static void client_Hello(Slack.HelloEventArgs e)
		{
			Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tHello");
		}


		private static void client_DoNotDisturbUpdatedUser(Slack.DoNotDisturbUpdatedUserEventArgs e)
		{
            if (e.dnd_status.dnd_enabled)
            {
                Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\t\t" + e.UserInfo.name + " " + e.dnd_status.next_dnd_start_ts.Date + " " + e.dnd_status.next_dnd_end_ts.Date);
            }
            else
            {
                Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\t\t" + e.UserInfo.name + " DND disabled");
            }
		}


		private static void client_DataReceived(String data)
		{
			//Console.WriteLine("Received: " + data);
		}


		private static void client_PresenceChanged(Slack.PresenceChangeEventArgs e)
		{
            Console.Write(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tPresence Changed.\t[");
            if (e.UserInfo == null)
            {
                Console.Write("Not Found (" + e.user + ")");
            }
            else
            { 
                Console.Write(e.UserInfo.name);
            }
            Console.WriteLine("] [" + e.presence + "]");
			String strChannel = client.IM.Open(e.user).ChannelID;
			if (strChannel.Length == 0)
			{
				return;
			}
			String strMessage = "";
			switch (e.presence)
			{
				case "active":
                    if (!dctAccounts.ContainsKey(e.UserInfo.name))
                    {
                        strMessage = "Welcome " + e.UserInfo.real_name + "\nWould you like to associate your account with a Time Tracker account?\nFor help type tt /?)";
                    }
                    else
                    {
                        TimeTrackerAuthentication auth = dctAccounts[e.UserInfo.name];
                        if (auth.Silence)
                        {
                            return;
                        }
                        if (dctAccounts[e.UserInfo.name].Token.Length == 0)
                        {
                            strMessage = "Welcome " + e.UserInfo.real_name + "\nWould you like to associate your account with a Time Tracker account?\nFor help type tt /?)";
                        }
                        else
                        {
                            strMessage = TimeTracker_UserActive(e.UserInfo.name, e.UserInfo.real_name);
					    }
                    }
					break;
				case "away":
					if (dctAccounts.ContainsKey(e.UserInfo.name))
					{
						strMessage = TimeTracker_UserInactive(e.UserInfo.name, e.UserInfo.real_name);
					}
					break;
				default:
					// do nothing
					break;
			}
			if (strMessage.Length == 0)
			{
				return;
			}
			Slack.Chat.PostMessageArguments args = new Slack.Chat.PostMessageArguments();
			args.channel = strChannel;
			args.text = strMessage;
			client.Chat.PostMessage(args);
		}


		private static void client_UserTyping(Slack.UserTypingEventArgs e)
		{
			Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tUser Typing.\t\t[" + e.UserInfo.name + "] [" + e.channel + "]");
		}


		private static void client_Message(Slack.MessageEventArgs e)
		{
			if (e.user == null)
			{
				return;
			}
			Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tMessage.\t\t[" + e.UserInfo.name + "] [" + e.text + "]");
			Process_Message(e.UserInfo.name, e.channel, e.text);
        }


		private static void client_MessageEdit(Slack.MessageEditEventArgs e)
		{
			Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tMessage Edit. [" + e.UserInfo.name + "] [" + e.message.text + "]");
			Process_Message(e.UserInfo.name, e.channel, e.message.text);
        }


		#endregion


		//This example method processes commands from users and optionally sends a message back to the user
		private static void Process_Message(String userName, String channel, String text)
		{
			const Int32 MAX_NEWS_ITEMS = 10;
			Slack.Chat.PostMessageArguments args = new Slack.Chat.PostMessageArguments();
			args.channel = channel;
			text = text.Trim().ToLower();
			switch (text)
			{
                case "silence":
                    Silence(userName, channel);
                    break;
                case "time":
                    args.text = "Server time is: " + System.DateTime.Now.ToString();
                    client.Chat.PostMessage(args);
                    break;
                case "local news":
					args.text = "";
					System.Net.WebClient wcLocalNews = new System.Net.WebClient();
					String strLocalNewsXML = wcLocalNews.DownloadString("http://www.inkfreenews.com/feed/");
					System.Xml.XmlDocument xmlLocalNews = new System.Xml.XmlDocument();
					xmlLocalNews.LoadXml(strLocalNewsXML);
					Int32 intLocalNewsRemaining = MAX_NEWS_ITEMS;
					foreach (System.Xml.XmlNode xmlNode in xmlLocalNews.SelectNodes("rss/channel/item"))
					{
						intLocalNewsRemaining--;
						if (intLocalNewsRemaining == 0)
						{
							break;
						}
						args.text +=
							xmlNode.SelectSingleNode("title").InnerText + "\r\n\t" +
							xmlNode.SelectSingleNode("link").InnerText + "\r\n";
					}
					client.Chat.PostMessage(args);
					break;
				case "news":
					args.text = "";
					System.Net.WebClient wcReutersTechNews = new System.Net.WebClient();
					String strReutersTechNewsXML = wcReutersTechNews.DownloadString("http://feeds.reuters.com/reuters/technologyNews");
					System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
					xmlDoc.LoadXml(strReutersTechNewsXML);
					Int32 intReutersTechNewsRemaining = MAX_NEWS_ITEMS;
					foreach (System.Xml.XmlNode xmlNode in xmlDoc.SelectNodes("rss/channel/item"))
					{
						intReutersTechNewsRemaining--;
						if (intReutersTechNewsRemaining == 0)
						{
							break;
						}
						args.text +=
							xmlNode.SelectSingleNode("title").InnerText + "\r\n\t" +
							xmlNode.SelectSingleNode("link").InnerText + "\r\n";
					}
					client.Chat.PostMessage(args);
					break;
				default:
					TimeTracker(userName, channel, text);
					break;
			}
		}


        private static void Silence(String strUserName, String strChannel)
        {
            TimeTrackerAuthentication auth;

            if (dctAccounts.ContainsKey(strUserName))
            {
                auth = dctAccounts[strUserName];
            }
            else
            {
                auth = new TimeTrackerAuthentication();
                dctAccounts.Add(strUserName, auth);
            }
            auth.Silence = !auth.Silence;
            dctAccounts[strUserName] = auth;
            TimeTracker_SaveAccounts();
            Slack.Chat.PostMessageArguments args = new Slack.Chat.PostMessageArguments();
            args.channel = strChannel;
            if (auth.Silence)
            {
                args.text = "I won't bother you until you unsilence me.";
            }
            else
            {
                args.text = "I will now prompt you regarding time tracking.";
            }
            client.Chat.PostMessage(args);
        }


        //Below is an example integration between slack and the time tracker service
        //for more information visit: http://timetracker.services.peakey.com/about/
        #region TimeTracker Integration


        private const String TIME_TRACKER_API_ENDPOINT = "http://timetracker.services.peakey.com/api";


		public class TimeTrackerAuthentication
		{
			public String EmailAddress;
			public String Token;
            public Boolean Silence;
		}


        private static Dictionary<String, TimeTrackerAuthentication> dctAccounts;


        private static void TimeTracker_LoadAccounts()
		{
            dctAccounts = new Dictionary<string, TimeTrackerAuthentication>();
            System.IO.IsolatedStorage.IsolatedStorageFile isoStore;
			isoStore = System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(System.IO.IsolatedStorage.IsolatedStorageScope.User | System.IO.IsolatedStorage.IsolatedStorageScope.Assembly, null, null);
			System.IO.IsolatedStorage.IsolatedStorageFileStream fsSFS;
			if (!isoStore.FileExists("settings.txt"))
			{
				return;
			}
			fsSFS = new System.IO.IsolatedStorage.IsolatedStorageFileStream("settings.txt", System.IO.FileMode.Open, isoStore);
			System.IO.StreamReader srSFS = new System.IO.StreamReader(fsSFS);
			String strFile = srSFS.ReadToEnd();
			String[] arrLines = strFile.Split(new char[] { '\r', '\n' });
			foreach (String strLine in arrLines)
			{
				if (strLine.Trim().Length == 0)
				{
					continue;
				}
				String[] arrAccount = strLine.Split('|');
				TimeTrackerAuthentication auth = new TimeTrackerAuthentication();
				auth.EmailAddress = arrAccount[1];
				auth.Token = arrAccount[2];
                auth.Silence = false;
                if (arrAccount.Length >= 4)
                {
                    if (!Boolean.TryParse(arrAccount[3], out auth.Silence))
                        {
                        auth.Silence = false;
                    }
                }
				dctAccounts.Add(arrAccount[0], auth);
			}
			srSFS.Close();
			srSFS.Dispose();
			srSFS = null;
			fsSFS.Close();
			fsSFS.Dispose();
			fsSFS = null;
		}


		private static void TimeTracker_SaveAccounts()
		{
			System.IO.IsolatedStorage.IsolatedStorageFile isoStore;
			isoStore = System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(System.IO.IsolatedStorage.IsolatedStorageScope.User | System.IO.IsolatedStorage.IsolatedStorageScope.Assembly, null, null);
			System.IO.IsolatedStorage.IsolatedStorageFileStream fsSFS;
			fsSFS = new System.IO.IsolatedStorage.IsolatedStorageFileStream("settings.txt", System.IO.FileMode.Create, isoStore);
			System.IO.StreamWriter swSFS = new System.IO.StreamWriter(fsSFS);
			foreach (String strAccount in dctAccounts.Keys)
			{
				TimeTrackerAuthentication auth;
				auth = dctAccounts[strAccount];
				swSFS.WriteLine(strAccount + "|" + auth.EmailAddress + "|" + auth.Token + "|" + auth.Silence);
			}
			swSFS.Flush();
			swSFS.Close();
			swSFS.Dispose();
			swSFS = null;
			fsSFS.Close();
			fsSFS.Dispose();
			fsSFS = null;
		}


		private static bool TimeTracker(String strUserName, String strChannel, String strCommand)
		{
			if (!strCommand.StartsWith("tt"))
			{
				return false;
			}
			strCommand = strCommand.ToLower();
			strCommand = strCommand.Substring(2).Trim(new char[]{ ' ', '/'});
			String[] arrParams = strCommand.Split(new char[] { '/', ':' });
			TimeTrackerAuthentication auth = new TimeTrackerAuthentication();
			if (!dctAccounts.ContainsKey(strUserName))
			{
                if (arrParams[0].Trim() == "associate")
                {
                    auth = Associate(strUserName, strChannel, arrParams);
                }
                if (arrParams[0].Trim() == "silence")
                {
                    auth = Associate(strUserName, strChannel, arrParams);
                }
                else if (arrParams[0].Trim() == "?")
				{
					Send_Help(strChannel, "Available commands:");
				}
				else
				{
					Slack.Chat.PostMessageArguments args2 = new Slack.Chat.PostMessageArguments();
					args2.channel = strChannel;
					args2.text = "Your slack account is not associated with a time tracker account";
					client.Chat.PostMessage(args2);
				}
				return true;
			}
			auth = dctAccounts[strUserName];
			Slack.Chat.PostMessageArguments args = new Slack.Chat.PostMessageArguments();
			args.channel = strChannel;
			switch (arrParams[0].Trim())
			{
				case "?":
					Send_Help(strChannel, "Available commands:");
					return true;
				case "associate":
					auth = Associate(strUserName, strChannel, arrParams);
					return true;
				case "current":
					args.text = TimeTracker_Current(auth);
					break;
				case "mytime":
					args.text = TimeTracker_MyTime(auth);
					break;
				case "project":
					if (arrParams.Length == 1)
					{
						Send_Help(strChannel, "Invalid syntax:");
						return false;
					}
					args.text = TimeTracker_Project(auth, arrParams[1].Trim());
					break;
				case "today":
					args.text = TimeTracker_Today(auth);
					break;
				default:
					args.text = TimeTracker_InOUT(auth, strCommand);
					break;
			}
			client.Chat.PostMessage(args);
			return true;
		}


		private static TimeTrackerAuthentication Associate(String strUserName, String strChannel, String[] arrParams)
		{
			TimeTrackerAuthentication auth = new TimeTrackerAuthentication();
			if (arrParams.Length < 5)
			{
				Send_Help(strChannel, "Invalid syntax: ");
                return null;
			}
			if (arrParams[1] == "email")
			{
				auth.EmailAddress = arrParams[2];
			}
			else
			{
				auth.Token = arrParams[2];
			}
			if (arrParams[3] == "email")
			{
				auth.EmailAddress = arrParams[4];
			}
			else
			{
				auth.Token = arrParams[4];
			}
			if (!dctAccounts.ContainsKey(strUserName))
			{
				dctAccounts.Add(strUserName, auth);
			}
			else
			{
				dctAccounts[strUserName] = auth;
			}
			TimeTracker_SaveAccounts();
			Slack.Chat.PostMessageArguments args3 = new Slack.Chat.PostMessageArguments();
			args3.channel = strChannel;
			args3.text =
				"Your time tracker account has been associated with your slack account.";
			client.Chat.PostMessage(args3);
			return auth;
		}


        private static void Send_Help(String strChannel, String strAdditional)
		{
			Slack.Chat.PostMessageArguments args = new Slack.Chat.PostMessageArguments();
			args.channel = strChannel;
			args.text =
				strAdditional + "\r\n" +
                "Time\tDisplay current server time.\r\n" +
                "Silence\tToggle's my ability to talk with you.\r\n" +
                "News\tDisplay latest tech news from reuters.\r\n" +
                "Local News\tDisplay local news.\r\n" +
                "tt /associate /email:[your email] /token:[your token]\tAssociate your slack account with your time tracker account.\r\n" +
				"tt /current\tDisplays your current punch status.\r\n" +
				"tt /myTime\tDisplays your current cumulative time.\r\n" +
				"tt /project:[project name]\tDisplays current cumulative time for a project.\r\n" +
				"tt /today\tDisplays your time entries for the current day.\r\n" +
				"tt /[project name]\tClocks you in / out of a given project.";
			client.Chat.PostMessage(args);
		}


		public static String TimeTracker_InOUT(TimeTrackerAuthentication auth, String strCommand)
		{
			try
			{
				String strURL =
					TIME_TRACKER_API_ENDPOINT + "?" +
					"email=" + System.Web.HttpUtility.UrlEncode(auth.EmailAddress) +
					"&token=" + System.Web.HttpUtility.UrlEncode(auth.Token) +
					"&command=" + System.Web.HttpUtility.UrlEncode(strCommand);
				System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
				httpWebRequest.Method = "GET";
				String strResponse;
				System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
				using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
				{
					strResponse = streamReader.ReadToEnd();
				}
                dynamic Response = System.Web.Helpers.Json.Decode(strResponse);
                if (!Slack.Utility.TryGetProperty(Response, "success", false))
                {
                    return Slack.Utility.TryGetProperty(Response, "reason");
                }
                return Slack.Utility.TryGetProperty(Response, "information") + " [" + strCommand + "]";
			}
			catch (Exception ex)
			{
				throw new Exception("Could not get connection info.", ex);
			}
		}


        public static String TimeTracker_Current(TimeTrackerAuthentication auth)
        {
            try
            {
                String strURL =
					TIME_TRACKER_API_ENDPOINT + "?" +
					"email=" + System.Web.HttpUtility.UrlEncode(auth.EmailAddress) +
					"&token=" + System.Web.HttpUtility.UrlEncode(auth.Token) +
					"&command=current";
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
                httpWebRequest.Method = "GET";
                String strResponse = "";
                System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    strResponse = streamReader.ReadToEnd();
                }
                String strOUT = "";
                dynamic Response = System.Web.Helpers.Json.Decode(strResponse);
                if (Slack.Utility.TryGetProperty(Response, "success", false))
                {
                    if (Slack.Utility.TryGetProperty(Response, "clockedIn", false))
                    {
                        strOUT +=
                            Slack.Utility.TryGetProperty(Response, "start") + "\t" +
                            Slack.Utility.TryGetProperty(Response, "end") + "\t" +
                            "Hours: " + (Math.Round(((Double)Slack.Utility.TryGetProperty(Response, "hours")), 2).ToString()).PadLeft(5, '0') + "\t" +
                            Slack.Utility.TryGetProperty(Response, "project") + "\r\n";
                    }
                    else
                    {
                        strOUT = "You are not currently clocked in.";
                    }
                }
                else
                {
                    strOUT = "Could not get current time information.\r\n" + Slack.Utility.TryGetProperty(Response, "reason");
                }

                return strOUT;
            }
            catch (Exception ex)
            {
                throw new Exception("Could get current time information.", ex);
            }
        }


        public static String TimeTracker_MyTime(TimeTrackerAuthentication auth)
        {
            try
            {
                String strURL =
					TIME_TRACKER_API_ENDPOINT + "?" +
					"email=" + System.Web.HttpUtility.UrlEncode(auth.EmailAddress) +
					"&token=" + System.Web.HttpUtility.UrlEncode(auth.Token) +
					"&command=myTime";
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
                httpWebRequest.Method = "GET";
                String strResponse = "";
                System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    strResponse = streamReader.ReadToEnd();
                }
                String strOUT = "";
                dynamic Response = System.Web.Helpers.Json.Decode(strResponse);
                if (Slack.Utility.TryGetProperty(Response, "success", false))
                {
                    strOUT +=
                        "Hours Today: " + Math.Round(((Double)Slack.Utility.TryGetProperty(Response, "todayHours")), 2).ToString() + "\r\n" +
                        "Hours This Week: " + Math.Round(((Double)Slack.Utility.TryGetProperty(Response, "weekHours")), 2).ToString() + "\r\n" +
                        "Hours This Pay Period: " + Math.Round(((Double)Slack.Utility.TryGetProperty(Response, "payPeriodHours")), 2).ToString() + "\r\n";
                }
                else
                {
                    strOUT = "Could not get current project information.\r\n" + Slack.Utility.TryGetProperty(Response, "reason");
                }

                return strOUT;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get current project information.", ex);
            }
        }


        public static String TimeTracker_Project(TimeTrackerAuthentication auth, String strProject)
        {
            try
            {
                String strURL =
					TIME_TRACKER_API_ENDPOINT + "?" +
					"email=" + System.Web.HttpUtility.UrlEncode(auth.EmailAddress) +
					"&token=" + System.Web.HttpUtility.UrlEncode(auth.Token) +
					"&command=project" +
                    "&project=" + strProject;
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
                httpWebRequest.Method = "GET";
                String strResponse = "";
                System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    strResponse = streamReader.ReadToEnd();
                }
                String strOUT = "";
                dynamic Response = System.Web.Helpers.Json.Decode(strResponse);
                if (Slack.Utility.TryGetProperty(Response, "success", false))
                {
                    strOUT +=
                        "Project: " + Slack.Utility.TryGetProperty(Response, "project") + "\r\n" +
                        "As of: " + Slack.Utility.TryGetProperty(Response, "asOf") + "\r\n" +
                        "Month Start Day: " + Slack.Utility.TryGetProperty(Response, "monthStartDay") + "\r\n" +
                        "Min. Hours: " + Slack.Utility.TryGetProperty(Response, "minHours") + "\r\n" +
                        "Max. Hours: " + Slack.Utility.TryGetProperty(Response, "maxHours") + "\r\n" +
                        "Current Hours: " + (Math.Round(((Double)Slack.Utility.TryGetProperty(Response, "currentHours")), 2).ToString()).PadLeft(5, '0') + "\r\n";
                }
                else
                {
                    strOUT = "Could not get current project information.\r\n" + Slack.Utility.TryGetProperty(Response, "reason");
                }

                return strOUT;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get current project information.", ex);
            }
        }


        public static String TimeTracker_Today(TimeTrackerAuthentication auth)
        {
            try
            {
                String strURL =
					TIME_TRACKER_API_ENDPOINT + "?" +
					"email=" + System.Web.HttpUtility.UrlEncode(auth.EmailAddress) +
					"&token=" + System.Web.HttpUtility.UrlEncode(auth.Token) +
					"&command=today";
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
                httpWebRequest.Method = "GET";
                String strResponse = "";
                System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    strResponse = streamReader.ReadToEnd();
                }
                String strOUT = "";
                dynamic Response = System.Web.Helpers.Json.Decode(strResponse);
                if (Slack.Utility.TryGetProperty(Response, "success", false))
                {
                    strOUT = "Today's Date: " + Slack.Utility.TryGetProperty(Response, "date") + "\r\n";
                    if (Slack.Utility.HasProperty(Response, "records"))
                    {
                        foreach (dynamic record in Response.records)
                        {
                            strOUT +=
                                Slack.Utility.TryGetProperty(record, "start") + "\t";
                            if (((String)Slack.Utility.TryGetProperty(record, "end")).Trim().Length == 0)
                            {
                                strOUT += "\t\t\t";
                            }
                            else
                            {
                                strOUT +=
                                    Slack.Utility.TryGetProperty(record, "end") + "\t";
                            }
                            strOUT +=
                                "Hours: " + (Math.Round(((Double)Slack.Utility.TryGetProperty(record, "hours")), 2).ToString()).PadLeft(5, ' ') + "\t" +
                                Slack.Utility.TryGetProperty(record, "project") + "\r\n";
                        }
                    }
                    strOUT += "Total Hours: " + Math.Round(((Double)Slack.Utility.TryGetProperty(Response, "totalHours")), 2).ToString();
                }
                else
                {
                    strOUT = "Could not get today's time information.\r\n" + Slack.Utility.TryGetProperty(Response, "reason");
                }

                return strOUT;
            }
            catch (Exception ex)
            {
                throw new Exception("Could get today's time information.", ex);
            }
        }


		public static String TimeTracker_UserActive(String strUserName, String strRealName)
		{
			try
			{
				if (!dctAccounts.ContainsKey(strUserName))
				{
					return "Welcome back " + strRealName + "\nWould you like to clock in?\nUse the \"tt\" command (for help type tt /?)";
				}
				TimeTrackerAuthentication auth = new TimeTrackerAuthentication();
				auth = dctAccounts[strUserName];
                if (auth.Silence)
                {
                    return "";
                }

                String strURL =
					TIME_TRACKER_API_ENDPOINT + "?" +
					"email=" + System.Web.HttpUtility.UrlEncode(auth.EmailAddress) +
					"&token=" + System.Web.HttpUtility.UrlEncode(auth.Token) +
					"&command=current";
				System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
				httpWebRequest.Method = "GET";
				String strResponse = "";
				System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
				using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
				{
					strResponse = streamReader.ReadToEnd();
				}
				String strOUT = "";
				dynamic Response = System.Web.Helpers.Json.Decode(strResponse);
				if (Slack.Utility.TryGetProperty(Response, "success", false))
				{
					if (Slack.Utility.TryGetProperty(Response, "clockedIn", false))
					{
						strOUT +=
							"Welcome back " + strRealName + " you are currently clocked in to " + Slack.Utility.TryGetProperty(Response, "project") + "\r\n" +
							Slack.Utility.TryGetProperty(Response, "start") + "\t" +
							Slack.Utility.TryGetProperty(Response, "end") + "\t" +
							"Hours: " + (Math.Round(((Double)Slack.Utility.TryGetProperty(Response, "hours")), 2).ToString()).PadLeft(5, '0') + "\r\n";
					}
				}
				else
				{
					strOUT = "Could not get current time information.\r\n" + Slack.Utility.TryGetProperty(Response, "reason");
				}

				return strOUT;
			}
			catch (Exception ex)
			{
				throw new Exception("Could not handle user active event.", ex);
			}
		}


		public static String TimeTracker_UserInactive(String strUserName, String strRealName)
		{
			try
			{
				if (!dctAccounts.ContainsKey(strUserName))
				{
					return "";
				}
				TimeTrackerAuthentication auth = new TimeTrackerAuthentication();
				auth = dctAccounts[strUserName];
                if (auth.Silence)
                {
                    return "";
                }

                String strURL =
					TIME_TRACKER_API_ENDPOINT + "?" +
					"email=" + System.Web.HttpUtility.UrlEncode(auth.EmailAddress) +
					"&token=" + System.Web.HttpUtility.UrlEncode(auth.Token) +
					"&command=current";
				System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
				httpWebRequest.Method = "GET";
				String strResponse = "";
				System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
				using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
				{
					strResponse = streamReader.ReadToEnd();
				}
				String strOUT = "";
				dynamic Response = System.Web.Helpers.Json.Decode(strResponse);
				if (Slack.Utility.TryGetProperty(Response, "success", false))
				{
					if (Slack.Utility.TryGetProperty(Response, "clockedIn", false))
					{
						strOUT +=
							"You are currently clocked in to " + Slack.Utility.TryGetProperty(Response, "project") + "\r\n" +
							Slack.Utility.TryGetProperty(Response, "start") + "\t" +
							Slack.Utility.TryGetProperty(Response, "end") + "\t" +
							"Hours: " + (Math.Round(((Double)Slack.Utility.TryGetProperty(Response, "hours")), 2).ToString()).PadLeft(5, '0') + "\r\n" +
							"Did you forget to clock out?\r\n";
					}
					else
					{
						strOUT = "";
					}
				}
				else
				{
					strOUT = "Could not get current time information.\r\n" + Slack.Utility.TryGetProperty(Response, "reason");
				}

				return strOUT;
			}
			catch (Exception ex)
			{
				throw new Exception("Could not handle user active event.", ex);
			}
		}


		#endregion


	}


}
