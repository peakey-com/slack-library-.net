using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack
{


    public class Client : IDisposable
    {


#region Declerations


#region Delegates


		public delegate void ServiceConnectedEventHandler();
		public delegate void ServiceConnectionFailedEventHandler();
		public delegate void ServiceDisconnectedEventHandler();

		public delegate void DataReceivedEventHandler(String data);
        public delegate void HelloEventHandler(HelloEventArgs e);
        public delegate void AccountsChangedEventHandler(AccountsChangedEventArgs e);
        public delegate void BotAddedEventHandler(BotAddedEventArgs e);
        public delegate void BotChangedEventHandler(BotChangedEventArgs e);
        public delegate void ChannelArchiveEventHandler(ChannelArchiveEventArgs e);
        public delegate void ChannelCreatedEventHandler(ChannelCreatedEventArgs e);
        public delegate void ChannelDeletedEventHandler(ChannelDeletedEventArgs e);
        public delegate void ChannelHistoryChangedEventHandler(ChannelHistoryChangedEventArgs e);
        public delegate void ChannelJoinedEventHandler(ChannelJoinedEventArgs e);
        public delegate void ChannelLeftEventHandler(ChannelLeftEventArgs e);
        public delegate void ChannelMarkedEventHandler(ChannelMarkedEventArgs e);
        public delegate void ChannelRenameEventHandler(ChannelRenameEventArgs e);
        public delegate void ChannelUnarchiveEventHandler(ChannelUnarchiveEventArgs e);
        public delegate void CommandsChangedEventHandler(CommandsChangedEventArgs e);
        public delegate void DoNotDistrubUpdatedEventHandler(DoNotDisturbUpdatedEventArgs e);
        public delegate void DoNotDistrubUpdatedUserEventHandler(DoNotDisturbUpdatedUserEventArgs e);
        public delegate void EmailDomainChangedEventHandler(EmailDomainChangedEventArgs e);
        public delegate void EmojiChangedEventHandler(EmojiChangedEventArgs e);
        public delegate void FileChangedEventHandler(FileChangeEventArgs e);
        public delegate void FileCommentAddedEventHandler(FileCommentAddedEventArgs e);
        public delegate void FileCommentEditedEventHandler(FileCommentEditedEventArgs e);
        public delegate void FileCommentDeletedEventHandler(FileCommentDeletedEventArgs e);
        public delegate void FileCreatedEventHandler(FileCreatedEventArgs e);
        public delegate void FileDeletedEventHandler(FileDeletedEventArgs e);
        public delegate void FilePrivateEventHandler(FilePrivateEventArgs e);
        public delegate void FilePublicEventHandler(FilePublicEventArgs e);
        public delegate void FileSharedEventHandler(FileSharedEventArgs e);
        public delegate void FileUnsharedEventHandler(FileUnsharedEventArgs e);
        public delegate void GroupArchiveEventHandler(GroupArchiveEventArgs e);
        public delegate void GroupCloseEventHandler(GroupCloseEventArgs e);
        public delegate void GroupHistoryChangedEventHandler(GroupHistoryChangedEventArgs e);
        public delegate void GroupJoinedEventHandler(GroupJoinedEventArgs e);
        public delegate void GroupLeftEventHandler(GroupLeftEventArgs e);
        public delegate void GroupMarkedEventHandler(GroupMarkedEventArgs e);
        public delegate void GroupOpenEventHandler(GroupOpenEventArgs e);
        public delegate void GroupRenameEventHandler(GroupRenameEventArgs e);
        public delegate void GroupUnarchiveEventHandler(GroupUnarchiveEventArgs e);
        public delegate void IMCloseEventHandler(IMCloseEventArgs e);
        public delegate void IMCreatedEventHandler(IMCreatedEventArgs e);
        public delegate void IMHistoryChangedEventHandler(IMHistoryChangedEventArgs e);
        public delegate void IMMarkedEventHandler(IMMarkedEventArgs e);
        public delegate void IMOpenEventHandler(IMOpenEventArgs e);
        public delegate void ManualPresenceChangeEventHandler(ManualPresenceChangeEventArgs e);
        public delegate void MessageEventHandler(MessageEventArgs e);
        public delegate void MessageEditEventHandler(MessageEditEventArgs e);
        public delegate void PinAddedEventHandler(PinAddedEventArgs e);
        public delegate void PinRemovedEventHandler(PinRemovedEventArgs e);
        public delegate void PrefChangedEventHandler(PrefChangedEventArgs e);
        public delegate void PresenceChangedEventHandler(PresenceChangeEventArgs e);
        public delegate void ReactionAddedEventHandler(ReactionAddedEventArgs e);
        public delegate void ReactionRemovedEventHandler(ReactionRemovedEventArgs e);
        public delegate void StarAddedEventHandler(StarAddedEventArgs e);
        public delegate void StarRemovedEventHandler(StarRemovedEventArgs e);
        public delegate void SubTeamCreatedEventHandler(SubTeamCreatedEventArgs e);
        public delegate void SubTeamSelfAddedEventHandler(SubTeamSelfAddedEventArgs e);
        public delegate void SubTeamSelfRemovedEventHandler(SubTeamSelfRemovedEventArgs e);
        public delegate void SubTeamUpdatedEventHandler(SubTeamUpdatedEventArgs e);
        public delegate void TeamDomainChangeEventHandler(TeamDomainChangeEventArgs e);
        public delegate void TeamJoinEventHandler(TeamJoinEventArgs e);
        public delegate void TeamMigrationStartedEventHandler(TeamMigrationStartedEventArgs e);
        public delegate void TeamPlanChangeEventHandler(TeamPlanChangeEventArgs e);
        public delegate void TeamPrefChangeEventHandler(TeamPrefChangeEventArgs e);
        public delegate void TeamProfileChangeEventHandler(TeamProfileChangeEventArgs e);
        public delegate void TeamProfileDeleteEventHandler(TeamProfileDeleteEventArgs e);
        public delegate void TeamProfileReorderEventHandler(TeamProfileReorderEventArgs e);
        public delegate void TeamRenameEventHandler(TeamRenameEventArgs e);
        public delegate void UserChangeEventHandler(UserChangeEventArgs e);
        public delegate void UserTypingEventHandler(UserTypingEventArgs e);


#endregion


        private const String URL_RTM_START = "https://slack.com/api/rtm.start";


        private String strApiKey;
        private RTM.MetaData rtmMetaData;
        private Slack.Channels.Collection _channels;
        private Slack.Chat _chat;
        private Slack.DND _dnd;
        private Slack.IM _im;
        private System.Net.WebSockets.ClientWebSocket webSocket;
		private System.Threading.Thread clientThread = null;


#region Public Events


		public event ServiceConnectedEventHandler ServiceConnected = null;
		public event ServiceConnectionFailedEventHandler ServiceConnectionFailed = null;
		public event ServiceDisconnectedEventHandler ServiceDisconnected = null;

		public event DataReceivedEventHandler DataReceived = null;
        public event HelloEventHandler Hello = null;
        public event AccountsChangedEventHandler AccountsChanged = null;
        public event BotAddedEventHandler BotAdded = null;
        public event BotChangedEventHandler BotChanged = null;
        public event ChannelArchiveEventHandler ChannelArchive = null;
        public event ChannelCreatedEventHandler ChannelCreated = null;
        public event ChannelDeletedEventHandler ChannelDeleted = null;
        public event ChannelHistoryChangedEventHandler ChannelHistoryChanged = null;
        public event ChannelJoinedEventHandler ChannelJoined = null;
        public event ChannelLeftEventHandler ChannelLeft = null;
        public event ChannelMarkedEventHandler ChannelMarked = null;
        public event ChannelRenameEventHandler ChannelRename = null;
        public event ChannelUnarchiveEventHandler ChannelUnarchive = null;
        public event CommandsChangedEventHandler CommandsChanged = null;
        public event DoNotDistrubUpdatedEventHandler DoNotDisturbUpdated = null;
        public event DoNotDistrubUpdatedUserEventHandler DoNotDisturbUpdatedUser = null;
        public event EmailDomainChangedEventHandler EmailDomainChanged = null;
        public event EmojiChangedEventHandler EmojiChanged = null;
        public event FileChangedEventHandler FileChanged = null;
        public event FileCommentAddedEventHandler FileCommentAdded = null;
        public event FileCommentEditedEventHandler FileCommentEdited = null;
        public event FileCommentDeletedEventHandler FileCommentDeleted = null;
        public event FileCreatedEventHandler FileCreated = null;
        public event FileDeletedEventHandler FileDeleted = null;
        public event FilePrivateEventHandler FilePrivate = null;
        public event FilePublicEventHandler FilePublic = null;
        public event FileSharedEventHandler FileShared = null;
        public event FileUnsharedEventHandler FileUnshared = null;
        public event GroupArchiveEventHandler GroupArchive = null;
        public event GroupCloseEventHandler GroupClose = null;
        public event GroupHistoryChangedEventHandler GroupHistoryChanged = null;
        public event GroupJoinedEventHandler GroupJoined = null;
        public event GroupLeftEventHandler GroupLeft = null;
        public event GroupMarkedEventHandler GroupMarked = null;
        public event GroupOpenEventHandler GroupOpen = null;
        public event GroupRenameEventHandler GroupRename = null;
        public event GroupUnarchiveEventHandler GroupUnarchive = null;
        public event IMCloseEventHandler IMClose = null;
        public event IMCreatedEventHandler IMCreated = null;
        public event IMHistoryChangedEventHandler IMHistoryChanged = null;
        public event IMMarkedEventHandler IMMarked = null;
        public event IMOpenEventHandler IMOpened = null;
        public event ManualPresenceChangeEventHandler ManualPresenceChange = null;
        public event MessageEventHandler Message = null;
        public event MessageEditEventHandler MesssageEdit = null;
        public event PinAddedEventHandler PinAdded = null;
        public event PinRemovedEventHandler PinRemoved = null;
        public event PrefChangedEventHandler PrefChanged = null;
        public event PresenceChangedEventHandler PresenceChanged = null;
        public event ReactionAddedEventHandler ReactionAdded = null;
        public event ReactionRemovedEventHandler ReactionRemoved = null;
        public event StarAddedEventHandler StarAdded = null;
        public event StarRemovedEventHandler StarRemoved = null;
        public event SubTeamCreatedEventHandler SubTeamCreated = null;
        public event SubTeamSelfAddedEventHandler SubTeamSelfAdded = null;
        public event SubTeamSelfRemovedEventHandler SubTeamSelfRemoved = null;
        public event SubTeamUpdatedEventHandler SubTeamUpdated = null;
        public event TeamDomainChangeEventHandler TeamDomainChange = null;
        public event TeamJoinEventHandler TeamJoin = null;
        public event TeamMigrationStartedEventHandler TeamMigrationStarted = null;
        public event TeamPlanChangeEventHandler TeamPlanChange = null;
        public event TeamPrefChangeEventHandler TeamPrefChange = null;
        public event TeamProfileChangeEventHandler TeamProfileChange = null;
        public event TeamProfileDeleteEventHandler TeamProfileDelete = null;
        public event TeamProfileReorderEventHandler TeamProfileReorder = null;
        public event TeamRenameEventHandler TeamRename = null;
        public event UserChangeEventHandler UserChange = null;
        public event UserTypingEventHandler UserTyping = null;


#endregion


#endregion


        public Client(String apiKey)
        {
            strApiKey = apiKey;
            _channels = new Slack.Channels.Collection(this);
            _chat = new Slack.Chat(this);
            _dnd = new Slack.DND(this);
            _im = new Slack.IM(this);
        }


        public void Dispose()
        {
            Disconnect();
        }


        private void _refreshRTMMetaData()
        {
			try
			{
				String strJSON = _downloadConnectionInfo();
				dynamic Message = null;
				Message = System.Web.Helpers.Json.Decode(strJSON);
				rtmMetaData = new RTM.MetaData(Message);
			}
			catch (Exception ex)
			{
				throw new Exceptions.ServiceDisconnectedException(ex);
			}
		}


        private String _downloadConnectionInfo()
        {
            try
            {
                String strURL = URL_RTM_START + "?token=" + strApiKey;
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
                httpWebRequest.Method = "GET";
                System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get connection info.", ex);
            }
        }


        public void Connect()
        {
			//run client on it's own thread
			try
			{
				if (clientThread != null)
				{
					clientThread.Abort();
				}
			}
			catch (System.Threading.ThreadAbortException)
			{
				//this is fine, client could be shut down
			}
			catch (Exception ex)
			{
				throw ex;
			}
			clientThread = new System.Threading.Thread(new System.Threading.ThreadStart(_connect));
			clientThread.Start();
		}


        private void _connect()
        {
			try
			{
				_refreshRTMMetaData();
			}
			catch (Exceptions.ServiceDisconnectedException ex)
			{
				if (ServiceDisconnected != null)
				{
					ServiceDisconnected();
				}
				return;
			}
			try
			{
				webSocket = new System.Net.WebSockets.ClientWebSocket();
				System.Threading.Tasks.Task tsk = webSocket.ConnectAsync(new Uri(rtmMetaData.url), System.Threading.CancellationToken.None);
				tsk.Wait();
			}
			catch (Exception)
			{
				if (ServiceConnectionFailed != null)
				{
					ServiceConnectionFailed();
				}
				return;	//bail....we can do nothing
			}
            try
			{
				if (ServiceConnected != null)
				{
					ServiceConnected();
				}
				_processMessages();
            }
			catch (System.Threading.ThreadAbortException)
			{
				//this is fine, client is being shutdown
			}
			catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }


        public void Disconnect()
        {
            _disconnect();
        }


        private void _disconnect()
        {
			try
			{
                if (webSocket != null)
				{
					System.Threading.Tasks.Task tsk = webSocket.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "", System.Threading.CancellationToken.None);
					tsk.Wait();
					webSocket.Dispose();
					webSocket = null;
				}
				if (ServiceDisconnected != null)
				{
					ServiceDisconnected();
				}
			}
			catch (System.Threading.ThreadAbortException)
			{
				//this is fine, client is being shutdown
			}
			catch (Exception)
			{
				//do nothing
			}
		}


		public String APIRequest(String strURL)
        {
            try
            {
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(strURL));
                httpWebRequest.Method = "GET";
                System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    String strResponse = streamReader.ReadToEnd();
                    if (DataReceived != null)
                    {
                        DataReceived(strResponse);
                    }
                    return strResponse;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not submit api request.", ex);
            }
        }


        public Boolean APITest()
        {
            try
            {
                String strResponse = APIRequest("https://slack.com/api/api.test");
                dynamic Response = System.Web.Helpers.Json.Decode(strResponse);
                return Response.ok;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not perform API test.", ex);
            }
        }


        public Slack.AuthTestResponse AuthTest()
        {
            //https://api.slack.com/methods/api.test
            dynamic Response;
            try
            {
                String strResponse = APIRequest("https://slack.com/api/auth.test?token=" + strApiKey);
                Response = System.Web.Helpers.Json.Decode(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not perform auth test.", ex);
            }
            CheckForError(Response);
            return new Slack.AuthTestResponse(Response);
        }


        public void CheckForError(dynamic Response)
        {
            if (!Slack.Utility.HasProperty(Response, "ok"))
            {   //no "ok" property.....that should probably be an exception too??
                return;
            }
            if (!Response.ok)
            {
                switch ((String)Response.error)
                {
                    case "account_inactive":
                        throw new Slack.Exceptions.AccountInactiveException();
                    case "already_archived":
                        throw new Slack.Exceptions.AlreadyArchivedException();
                    case "already_in_channel":
                        throw new Slack.Exceptions.AlreadyInChannelException();
                    case "cant_archive_general":
                        throw new Slack.Exceptions.CantArchiveGeneralException();
                    case "cant_delete_message":
                        throw new Slack.Exceptions.CantDeleteMessageException();
                    case "cant_invite":
                        throw new Slack.Exceptions.CantInviteException();
                    case "cant_invite_self":
                        throw new Slack.Exceptions.CantInviteSelfException();
                    case "cant_kick_self":
                        throw new Slack.Exceptions.CantKickSelfException();
                    case "cant_kick_from_general":
                        throw new Slack.Exceptions.CantKickFromGeneralException();
                    case "cant_kick_from_last_channel":
                        throw new Slack.Exceptions.CantKickFromLastChannelException();
                    case "cant_leave_general":
                        throw new Slack.Exceptions.CantLeaveGeneralException();
                    case "cant_update_message":
                        throw new Slack.Exceptions.CantUpdateMessageException();
                    case "channel_not_found":
                        throw new Slack.Exceptions.ChannelNotFoundException();
                    case "compliance_exports_prevent_deletion":
                        throw new Slack.Exceptions.ComplianceExportsPreventDeletionException();
                    case "edit_window_closed":
                        throw new Slack.Exceptions.EditWindowClosedException();
                    case "invalid_array_arg":
                        throw new Slack.Exceptions.InvalidArrayArgException();
                    case "invalid_auth":
                        throw new Slack.Exceptions.InvalidAuthException();
                    case "invalid_charset":
                        throw new Slack.Exceptions.InvalidCharsetException();
                    case "invalid_form_data":
                        throw new Slack.Exceptions.InvalidFormDataException();
                    case "invalid_post_type":
                        throw new Slack.Exceptions.InvalidPostTypeException();
                    case "invalid_timestamp":
                        throw new Slack.Exceptions.InvalidTimestampException();
                    case "invalid_ts_latest":
                        throw new Slack.Exceptions.InvalidTSLatestException();
                    case "invalid_ts_oldest":
                        throw new Slack.Exceptions.InvalidTSOldestException();
                    case "is_archived":
                        throw new Slack.Exceptions.IsArchivedException();
                    case "last_ra_channel":
                        throw new Slack.Exceptions.LastRestrictedAccountException();
                    case "message_not_found":
                        throw new Slack.Exceptions.MessageNotFoundException();
                    case "missing_duration":
                        throw new Slack.Exceptions.MissingDurationException();
                    case "missing_post_type":
                        throw new Slack.Exceptions.MissingPostTypeException();
                    case "msg_too_long":
                        throw new Slack.Exceptions.MessageTooLongException();
                    case "name_taken":
                        throw new Slack.Exceptions.NameTakenException();
                    case "no_channel":
                        throw new Slack.Exceptions.NoChannelException();
                    case "no_text":
                        throw new Slack.Exceptions.NoTextException();
                    case "not_archived":
                        throw new Slack.Exceptions.NotArchivedException();
                    case "not_authed":
                        throw new Slack.Exceptions.NotAuthedException();
                    case "not_in_channel":
                        throw new Slack.Exceptions.NotInChannelException();
                    case "rate_limited":
                        throw new Slack.Exceptions.RateLimitedException();
                    case "restricted_action":
                        throw new Slack.Exceptions.RestrictedActionException();
                    case "request_timeout":
                        throw new Slack.Exceptions.RequestTimeoutException();
                    case "too_long":
                        throw new Slack.Exceptions.TooLongException();
                    case "unknown_error":
                        throw new Slack.Exceptions.UnknownErrorException();
                    case "user_disabled":
                        throw new Slack.Exceptions.UserDisabledException();
                    case "user_is_bot":
                        throw new Slack.Exceptions.UserIsBotException();
                    case "user_is_restricted":
                        throw new Slack.Exceptions.UserIsRestrictedException();
                    case "user_is_ultra_restricted":
                        throw new Slack.Exceptions.UserIsUltraRestrictedException();
                    case "user_not_found":
                        throw new Slack.Exceptions.UserNotFoundException();
                    case "user_not_visible":
                        throw new Slack.Exceptions.UserNotVisibleException();
                }
            }
        }


        private void _processMessages()
        {
			try
			{
				String strMessage;
				while (1 == 1)
				{
					try
					{
						System.Threading.Tasks.Task<String> tsk = _readMessage();
						tsk.Wait();
						strMessage = tsk.Result;
						if (DataReceived != null)
						{
							DataReceived(strMessage);
						}
						dynamic Data = System.Web.Helpers.Json.Decode(strMessage);
						switch ((String)Data.type)
						{
							case "hello":
								HelloEventArgs helloEventArgs = new HelloEventArgs();
								if (Hello != null)
								{
									Hello(helloEventArgs);
								}
								break;
							case "accounts_changed":
								AccountsChangedEventArgs accountsChangedEventArgs = new AccountsChangedEventArgs(Data);
								if (AccountsChanged != null)
								{
									AccountsChanged(accountsChangedEventArgs);
								}
								break;
							case "bot_added":
								BotAddedEventArgs botAddedEventArgs = new BotAddedEventArgs(Data);
								if (BotAdded != null)
								{
									BotAdded(botAddedEventArgs);
								}
								break;
							case "bot_changed":
								BotChangedEventArgs botChangedEventArgs = new BotChangedEventArgs(Data);
								if (BotChanged != null)
								{
									BotChanged(botChangedEventArgs);
								}
								break;
							case "channel_archive":
								ChannelArchiveEventArgs channelArchiveEventArgs = new ChannelArchiveEventArgs(Data);
								if (ChannelArchive != null)
								{
									ChannelArchive(channelArchiveEventArgs);
								}
								break;
							case "channel_created":
								ChannelCreatedEventArgs channelCreatedEventArgs = new ChannelCreatedEventArgs(Data);
								if (ChannelCreated != null)
								{
									ChannelCreated(channelCreatedEventArgs);
								}
								break;
							case "channel_deleted":
								ChannelDeletedEventArgs channelDeletedEventArgs = new ChannelDeletedEventArgs(Data);
								if (ChannelDeleted != null)
								{
									ChannelDeleted(channelDeletedEventArgs);
								}
								break;
							case "channel_history_changed":
								ChannelHistoryChangedEventArgs channelHistoryChangedEventArgs = new ChannelHistoryChangedEventArgs(Data);
								if (ChannelHistoryChanged != null)
								{
									ChannelHistoryChanged(channelHistoryChangedEventArgs);
								}
								break;
							case "channel_joined":
								ChannelJoinedEventArgs channelJoinedEventArgs = new ChannelJoinedEventArgs(Data);
								if (ChannelJoined != null)
								{
									ChannelJoined(channelJoinedEventArgs);
								}
								break;
							case "channel_left":
								ChannelLeftEventArgs channelLeftEventArgs = new ChannelLeftEventArgs(Data);
								if (ChannelLeft != null)
								{
									ChannelLeft(channelLeftEventArgs);
								}
								break;
							case "channel_marked":
								ChannelMarkedEventArgs channelMarkedEventArgs = new ChannelMarkedEventArgs(Data);
								if (ChannelMarked != null)
								{
									ChannelMarked(channelMarkedEventArgs);
								}
								break;
							case "channel_rename":
								ChannelRenameEventArgs channelRenameEventArgs = new ChannelRenameEventArgs(Data);
								if (ChannelRename != null)
								{
									ChannelRename(channelRenameEventArgs);
								}
								break;
							case "channel_unarchive":
								ChannelUnarchiveEventArgs channelUnarchiveEventArgs = new ChannelUnarchiveEventArgs(Data);
								if (ChannelUnarchive != null)
								{
									ChannelUnarchive(channelUnarchiveEventArgs);
								}
								break;
							case "commands_changed":
								CommandsChangedEventArgs commandsChangedEventArgs = new CommandsChangedEventArgs(Data);
								if (CommandsChanged != null)
								{
									CommandsChanged(commandsChangedEventArgs);
								}
								break;
							case "dnd_updated":
								DoNotDisturbUpdatedEventArgs dndUpdatedEventArgs = new DoNotDisturbUpdatedEventArgs(Data);
								if (DoNotDisturbUpdated != null)
								{
									DoNotDisturbUpdated(dndUpdatedEventArgs);
								}
								break;
							case "dnd_updated_user":
								DoNotDisturbUpdatedUserEventArgs dndUpdatedUserEventArgs = new DoNotDisturbUpdatedUserEventArgs(this, Data);
								if (DoNotDisturbUpdatedUser != null)
								{
									DoNotDisturbUpdatedUser(dndUpdatedUserEventArgs);
								}
								break;
							case "email_domain_changed":
								EmailDomainChangedEventArgs emailDomainChangedEventArgs = new EmailDomainChangedEventArgs(Data);
								if (EmailDomainChanged != null)
								{
									EmailDomainChanged(emailDomainChangedEventArgs);
								}
								break;
							case "emoji_changed":
								EmojiChangedEventArgs emojiChangedEventArgs = new EmojiChangedEventArgs(Data);
								if (EmojiChanged != null)
								{
									EmojiChanged(emojiChangedEventArgs);
								}
								break;
							case "file_change":
								FileChangeEventArgs fileChangeEventArgs = new FileChangeEventArgs(Data);
								if (FileChanged != null)
								{
									FileChanged(fileChangeEventArgs);
								}
								break;
							case "file_comment_added":
								FileCommentAddedEventArgs fileCommentAddedEventArgs = new FileCommentAddedEventArgs(Data);
								if (FileCommentAdded != null)
								{
									FileCommentAdded(fileCommentAddedEventArgs);
								}
								break;
							case "file_comment_edited":
								FileCommentEditedEventArgs fileCommentEditedEventArgs = new FileCommentEditedEventArgs(Data);
								if (FileCommentEdited != null)
								{
									FileCommentEdited(fileCommentEditedEventArgs);
								}
								break;
							case "file_comment_deleted":
								FileCommentDeletedEventArgs fileCommentDeletedEventArgs = new FileCommentDeletedEventArgs(Data);
								if (FileCommentDeleted != null)
								{
									FileCommentDeleted(fileCommentDeletedEventArgs);
								}
								break;
							case "file_created":
								FileCreatedEventArgs fileCreatedEventArgs = new FileCreatedEventArgs(Data);
								if (FileCreated != null)
								{
									FileCreated(fileCreatedEventArgs);
								}
								break;
							case "file_deleted":
								FileDeletedEventArgs fileDeletedEventArgs = new FileDeletedEventArgs(Data);
								if (FileDeleted != null)
								{
									FileDeleted(fileDeletedEventArgs);
								}
								break;
							case "file_private":
								FilePrivateEventArgs filePrivateEventArgs = new FilePrivateEventArgs(Data);
								if (FilePrivate != null)
								{
									FilePrivate(filePrivateEventArgs);
								}
								break;
							case "file_public":
								FilePublicEventArgs filePublicEventArgs = new FilePublicEventArgs(Data);
								if (FilePublic != null)
								{
									FilePublic(filePublicEventArgs);
								}
								break;
							case "file_shared":
								FileSharedEventArgs fileSharedEventArgs = new FileSharedEventArgs(Data);
								if (FileShared != null)
								{
									FileShared(fileSharedEventArgs);
								}
								break;
							case "file_unshared":
								FileUnsharedEventArgs fileUnsharedEventArgs = new FileUnsharedEventArgs(Data);
								if (FileUnshared != null)
								{
									FileUnshared(fileUnsharedEventArgs);
								}
								break;
							case "group_archive":
								GroupArchiveEventArgs groupArchiveEventArgs = new GroupArchiveEventArgs(Data);
								if (GroupArchive != null)
								{
									GroupArchive(groupArchiveEventArgs);
								}
								break;
							case "group_close":
								GroupCloseEventArgs groupCloseEventArgs = new GroupCloseEventArgs(Data);
								if (GroupClose != null)
								{
									GroupClose(groupCloseEventArgs);
								}
								break;
							case "group_history_changed":
								GroupHistoryChangedEventArgs groupHistoryChangedEventArgs = new GroupHistoryChangedEventArgs(Data);
								if (GroupHistoryChanged != null)
								{
									GroupHistoryChanged(groupHistoryChangedEventArgs);
								}
								break;
							case "group_joined":
								GroupJoinedEventArgs groupJoinedEventArgs = new GroupJoinedEventArgs(Data);
								if (GroupJoined != null)
								{
									GroupJoined(groupJoinedEventArgs);
								}
								break;
							case "group_left":
								GroupLeftEventArgs groupLeftEventArgs = new GroupLeftEventArgs(Data);
								if (GroupLeft != null)
								{
									GroupLeft(groupLeftEventArgs);
								}
								break;
							case "group_marked":
								GroupMarkedEventArgs groupMarkedEventArgs = new GroupMarkedEventArgs(Data);
								if (GroupMarked != null)
								{
									GroupMarked(groupMarkedEventArgs);
								}
								break;
							case "group_open":
								GroupOpenEventArgs groupOpenEventArgs = new GroupOpenEventArgs(Data);
								if (GroupOpen != null)
								{
									GroupOpen(groupOpenEventArgs);
								}
								break;
							case "group_rename":
								GroupRenameEventArgs groupRenameEventArgs = new GroupRenameEventArgs(Data);
								if (GroupRename != null)
								{
									GroupRename(groupRenameEventArgs);
								}
								break;
							case "group_unarchive":
								GroupUnarchiveEventArgs groupUnarchiveEventArgs = new GroupUnarchiveEventArgs(Data);
								if (GroupUnarchive != null)
								{
									GroupUnarchive(groupUnarchiveEventArgs);
								}
								break;
							case "im_close":
								IMCloseEventArgs imCloseEventArgs = new IMCloseEventArgs(Data);
								if (IMClose != null)
								{
									IMClose(imCloseEventArgs);
								}
								break;
							case "im_created":
								IMCreatedEventArgs imCreatedEventArgs = new IMCreatedEventArgs(Data);
								if (IMCreated != null)
								{
									IMCreated(imCreatedEventArgs);
								}
								break;
							case "im_history_changed":
								IMHistoryChangedEventArgs imHistoryChangedEventArgs = new IMHistoryChangedEventArgs(Data);
								if (IMHistoryChanged != null)
								{
									IMHistoryChanged(imHistoryChangedEventArgs);
								}
								break;
							case "im_marked":
								IMMarkedEventArgs imMarkedEventArgs = new IMMarkedEventArgs(Data);
								if (IMMarked != null)
								{
									IMMarked(imMarkedEventArgs);
								}
								break;
							case "im_open":
								IMOpenEventArgs imOpenEventArgs = new IMOpenEventArgs(Data);
								if (IMOpened != null)
								{
									IMOpened(imOpenEventArgs);
								}
								break;
							case "manual_presence_change":
								ManualPresenceChangeEventArgs manualPresenceChangeEventArgs = new ManualPresenceChangeEventArgs(Data);
								if (ManualPresenceChange != null)
								{
									ManualPresenceChange(manualPresenceChangeEventArgs);
								}
								break;
							case "message":
								if (Data.previous_message == null)
								{
									MessageEventArgs messagEventArgs = new MessageEventArgs(this, Data);
									if (Message != null)
									{
										Message(messagEventArgs);
									}
								}
								else
								{
									MessageEditEventArgs messageEditEventArgs = new MessageEditEventArgs(this, Data);
									if (MesssageEdit != null)
									{
										MesssageEdit(messageEditEventArgs);
									}
								}
								break;
							case "pin_added":
								PinAddedEventArgs pinAddedEventArgs = new PinAddedEventArgs(Data);
								if (PinAdded != null)
								{
									PinAdded(pinAddedEventArgs);
								}
								break;
							case "pin_removed":
								PinRemovedEventArgs pinRemovedEventArgs = new PinRemovedEventArgs(Data);
								if (PinRemoved != null)
								{
									PinRemoved(pinRemovedEventArgs);
								}
								break;
							case "pref_changed":
								PrefChangedEventArgs prefChangedEventArgs = new PrefChangedEventArgs(Data);
								if (PrefChanged != null)
								{
									PrefChanged(prefChangedEventArgs);
								}
								break;
							case "presence_change":
								PresenceChangeEventArgs presenceChangeEventArgs = new PresenceChangeEventArgs(this, Data);
								if (PresenceChanged != null)
								{
									PresenceChanged(presenceChangeEventArgs);
								}
								break;
							case "reaction_added":
								ReactionAddedEventArgs reactionAddedEventArgs = new ReactionAddedEventArgs(Data);
								if (ReactionAdded != null)
								{
									ReactionAdded(reactionAddedEventArgs);
								}
								break;
							case "reaction_removed":
								ReactionRemovedEventArgs reactionRemovedEventArgs = new ReactionRemovedEventArgs(Data);
								if (ReactionRemoved != null)
								{
									ReactionRemoved(reactionRemovedEventArgs);
								}
								break;
							case "star_added":
								StarAddedEventArgs starAddedEventArgs = new StarAddedEventArgs(Data);
								if (StarAdded != null)
								{
									StarAdded(starAddedEventArgs);
								}
								break;
							case "star_removed":
								StarRemovedEventArgs starRemovedEventArgs = new StarRemovedEventArgs(Data);
								if (StarRemoved != null)
								{
									StarRemoved(starRemovedEventArgs);
								}
								break;
							case "subteam_created":
								SubTeamCreatedEventArgs subTeamCreatedEventArgs = new SubTeamCreatedEventArgs(Data);
								if (SubTeamCreated != null)
								{
									SubTeamCreated(subTeamCreatedEventArgs);
								}
								break;
							case "subteam_self_added":
								SubTeamSelfAddedEventArgs subTeamSelfAddedEventArgs = new SubTeamSelfAddedEventArgs(Data);
								if (SubTeamSelfAdded != null)
								{
									SubTeamSelfAdded(subTeamSelfAddedEventArgs);
								}
								break;
							case "subteam_self_removed":
								SubTeamSelfRemovedEventArgs subTeamSelfRemovedEventArgs = new SubTeamSelfRemovedEventArgs(Data);
								if (SubTeamSelfRemoved != null)
								{
									SubTeamSelfRemoved(subTeamSelfRemovedEventArgs);
								}
								break;
							case "subteam_updated":
								SubTeamUpdatedEventArgs subTeamUpdatedEventArgs = new SubTeamUpdatedEventArgs(Data);
								if (SubTeamUpdated != null)
								{
									SubTeamUpdated(subTeamUpdatedEventArgs);
								}
								break;
							case "team_domain_change":
								TeamDomainChangeEventArgs teamDomainChangeEventArgs = new TeamDomainChangeEventArgs(Data);
								if (TeamDomainChange != null)
								{
									TeamDomainChange(teamDomainChangeEventArgs);
								}
								break;
							case "team_join":
								TeamJoinEventArgs teamJoinEventArgs = new TeamJoinEventArgs(Data);
								if (TeamJoin != null)
								{
									TeamJoin(teamJoinEventArgs);
								}
								break;
							case "team_migration_started":
								TeamMigrationStartedEventArgs teamMigrationStartedEventArgs = new TeamMigrationStartedEventArgs(Data);
								if (TeamMigrationStarted != null)
								{
									TeamMigrationStarted(teamMigrationStartedEventArgs);
								}
								break;
							case "team_plan_change":
								TeamPlanChangeEventArgs teamPlanChangeEventArgs = new TeamPlanChangeEventArgs(Data);
								if (TeamPlanChange != null)
								{
									TeamPlanChange(teamPlanChangeEventArgs);
								}
								break;
							case "team_pref_change":
								TeamPrefChangeEventArgs teamPrefChangeEventArgs = new TeamPrefChangeEventArgs(Data);
								if (TeamPrefChange != null)
								{
									TeamPrefChange(teamPrefChangeEventArgs);
								}
								break;
							case "team_profile_change":
								TeamProfileChangeEventArgs teamProfileChangeEventArgs = new TeamProfileChangeEventArgs(Data);
								if (TeamProfileChange != null)
								{
									TeamProfileChange(teamProfileChangeEventArgs);
								}
								break;
							case "team_profile_delete":
								TeamProfileDeleteEventArgs teamProfileDeleteEventArgs = new TeamProfileDeleteEventArgs(Data);
								if (TeamProfileDelete != null)
								{
									TeamProfileDelete(teamProfileDeleteEventArgs);
								}
								break;
							case "team_profile_reorder":
								TeamProfileReorderEventArgs teamProfileReorderEventArgs = new TeamProfileReorderEventArgs(Data);
								if (TeamProfileReorder != null)
								{
									TeamProfileReorder(teamProfileReorderEventArgs);
								}
								break;
							case "team_rename":
								TeamRenameEventArgs teamRenameEventArgs = new TeamRenameEventArgs(Data);
								if (TeamRename != null)
								{
									TeamRename(teamRenameEventArgs);
								}
								break;
							case "user_change":
								UserChangeEventArgs userChangeEventArgs = new UserChangeEventArgs(Data);
								if (UserChange != null)
								{
									UserChange(userChangeEventArgs);
								}
								break;
							case "user_typing":
								UserTypingEventArgs userTypingEventArgs = new UserTypingEventArgs(this, Data);
								if (UserTyping != null)
								{
									UserTyping(userTypingEventArgs);
								}
								break;
							default: //null
								break;
						}
					}
					catch (System.AggregateException ex)
					{
						throw ex.InnerException;
					}
					catch (Exceptions.ServiceDisconnectedException ex)
					{
						throw ex;
					}
					catch (System.Threading.ThreadAbortException ex)
					{
						throw ex;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
					}
				}
			}
			catch (Exceptions.ServiceDisconnectedException)
			{
				//do nothing....normal for disconnected slack service
			}
			catch (System.Threading.ThreadAbortException)
			{
				//this is fine, client is being shutdown
			}
			catch (Exception)
			{

			}
        }


        private async Task<String> _readMessage()
        {
            try
			{
				ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[8192]);

				System.Net.WebSockets.WebSocketReceiveResult result = null;

				while (1 == 1)
				{
					using (var ms = new System.IO.MemoryStream())
					{
						do
						{
							result = await webSocket.ReceiveAsync(buffer, System.Threading.CancellationToken.None);
							ms.Write(buffer.Array, buffer.Offset, result.Count);
						} while (!result.EndOfMessage);

						ms.Seek(0, System.IO.SeekOrigin.Begin);

						if (result.MessageType == System.Net.WebSockets.WebSocketMessageType.Text)
						{
							using (var reader = new System.IO.StreamReader(ms, Encoding.UTF8))
							{
								// do stuff
							}
						}
						Byte[] bytMessage = ms.ToArray();
						return System.Text.ASCIIEncoding.ASCII.GetString(bytMessage);
					}
				}
			}
			catch (System.Net.WebSockets.WebSocketException ex)
			{
				_disconnect();
                throw new Exceptions.ServiceDisconnectedException(ex);
			}
			catch (Exception ex)
			{
				_disconnect();
                throw new Exceptions.ServiceDisconnectedException(ex);
			}
        }


        public String APIKey
        {
            get
            {
                return strApiKey;
            }
        }


        public RTM.MetaData MetaData
        {
            get
            {
                return rtmMetaData;
            }
        }


        public Slack.Channels.Collection Channels
        {
            get
            {
                return _channels;
            }
        }


        public Slack.Chat Chat
        {
            get
            {
                return _chat;
            }
        }


        public Slack.DND DND
        {
            get
            {
                return _dnd;
            }
        }


        public Slack.IM IM
        {
            get
            {
                return _im;
            }
        }


    }


}
