using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Synzeal_Inventory.Models;
using Microsoft.AspNet.SignalR.Hubs;
using ChatApp.Domain.Concrete;
using Microsoft.AspNet.SignalR.Transports;
using Synzeal_Inventory.Models.Chat;
using System.Threading.Tasks;
using Synzeal_Inventory.Repository;
using Synzeal_Inventory.Entity;

namespace Synzeal_Inventory.Models
{
    public class ChatHub : Hub
    {
        public SynzealLiveEntities db = new SynzealLiveEntities();
        public IOperationRepository _operationRepo;
        //new OperationRepository(db);
        public ChatHub()
        {
            this._operationRepo = new OperationRepository(db);
        }
        
        public override Task OnConnected()
        {
            //var userID = Context.QueryString["UserID"];
            var userID = Context.RequestCookies["UserId"].Value;
            if (userID != null)
            {
                int uId = Convert.ToInt32(userID);
                _operationRepo.SaveUserOnlineStatus(new OnlineUser { UserID = uId, ConnectionID = Context.ConnectionId, IsOnline = true });
                RefreshOnlineUsers(uId);
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var userID = Context.RequestCookies["UserId"].Value;
            if (userID != null)
            {
                int uId = Convert.ToInt32(userID);
                _operationRepo.SaveUserOnlineStatus(new OnlineUser { UserID = uId, ConnectionID = Context.ConnectionId, IsOnline = false });
                RefreshOnlineUsers(uId);
            }
            return base.OnDisconnected(stopCalled);
        }
        public List<string> GetActiveConnectionIds(List<string> connectionIds)
        {
            var heartBeat = GlobalHost.DependencyResolver.Resolve<ITransportHeartbeat>();
            var connections = heartBeat.GetConnections();
            if (connections != null && connectionIds != null)
            {
                var filterdConnectionIds = connections.Where(m => connectionIds.Contains(m.ConnectionId)).Select(m => m.ConnectionId).ToList();
                return filterdConnectionIds;
            }
            return connectionIds;
        }
        public void RefreshOnlineUsers(int userID)
        {
            var users = _operationRepo.GetOnlineFriends(userID);
            RefreshOnlineUsersByConnectionIds(users.SelectMany(m => m.ConnectionID).ToList(), userID);
        }
        public void RefreshOnlineUsersByConnectionIds(List<string> connectionIds, int userID = 0)
        {
            Clients.Clients(connectionIds).RefreshOnlineUsers();
            if (userID > 0)
            {
                var onlineStatus = _operationRepo.GetUserOnlineStatus(userID);
                if (onlineStatus != null)
                {
                    Clients.Clients(connectionIds).RefreshOnlineUserByUserID(userID, onlineStatus.IsOnline, Convert.ToString(onlineStatus.LastUpdationTime));
                }
            }
        }
        public void SendRequest(int userID, int loggedInUserID, string type)
        {
            //_operationRepo.SendFriendRequest(userID, loggedInUserID);
            SendNotification(loggedInUserID, userID, type);
        }
        public void SendNotification(int fromUserID, int toUserID, string notificationType)
        {
            int notificationID = _operationRepo.SaveUserNotification(notificationType, fromUserID, toUserID);
            var connectionId = _operationRepo.GetUserConnectionID(toUserID);
            if (connectionId != null && connectionId.Count() > 0)
            {
                var userInfo = CommonFunctions.GetUserModel(fromUserID);
                int notificationCounts = _operationRepo.GetUserNotificationCounts(toUserID);
                Clients.Clients(connectionId).ReceiveNotification(notificationType, userInfo, notificationID, notificationCounts);
            }
        }
        public void SendResponseToRequest(int requestorID, string requestResponse, int endUserID)
        {
            //var notificationID = _operationRepo.ResponseToFriendRequest(requestorID, requestResponse, endUserID);
            //if (notificationID > 0)
            //{
            //    var connectionId = _operationRepo.GetUserConnectionID(endUserID);
            //    if (connectionId != null && connectionId.Count() > 0)
            //    {
            //        Clients.Clients(connectionId).RemoveNotification(notificationID);
            //    }
            //}
            //if (requestResponse == "Accepted")
            //{
            //    SendNotification(endUserID, requestorID, "FriendRequestAccepted");
            //    List<string> connectionIds = _operationRepo.GetUserConnectionID(new int[] { endUserID, requestorID });
            //    RefreshOnlineUsersByConnectionIds(connectionIds);
            //}
        }
        public void RefreshNotificationCounts(int toUserID)
        {
            var connectionId = _operationRepo.GetUserConnectionID(toUserID);
            if (connectionId != null && connectionId.Count() > 0)
            {
                int notificationCounts = _operationRepo.GetUserNotificationCounts(toUserID);
                Clients.Clients(connectionId).RefreshNotificationCounts(notificationCounts);
            }
        }
        public void ChangeNotitficationStatus(string notificationIds, int toUserID)
        {
            if (!string.IsNullOrEmpty(notificationIds))
            {
                string[] arrNotificationIds = notificationIds.Split(',');
                int[] ids = arrNotificationIds.Select(m => Convert.ToInt32(m)).ToArray();
                _operationRepo.ChangeNotificationStatus(ids);
                RefreshNotificationCounts(toUserID);
            }
        }
        public void UnfriendUser(int friendMappingID)
        {
            //var friendMapping = _operationRepo.RemoveFriendMapping(friendMappingID);
            //if (friendMapping != null)
            //{
            //    List<string> connectionIds = _operationRepo.GetUserConnectionID(new int[] { friendMapping.EndUserID, friendMapping.RequestorUserID });
            //    RefreshOnlineUsersByConnectionIds(connectionIds);
            //}
        }
        public void SendMessage(int fromUserId, int toUserId, string message, string fromUserName, string fromUserProfilePic, string toUserName, string toUserProfilePic)
        {
            ChatMessage objentity = new ChatMessage();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.FromUserID = fromUserId;
            objentity.IsActive = true;
            objentity.Message = message;
            objentity.ViewedOn = System.DateTime.Now;
            objentity.Status = "Sent";
            objentity.ToUserID = toUserId;
            objentity.UpdatedOn = System.DateTime.Now;
            var obj = _operationRepo.SaveChatMessage(objentity);
            var messageRow = CommonFunctions.GetMessageModel(obj);
            List<string> connectionIds = _operationRepo.GetUserConnectionID(new int[] { fromUserId, toUserId });
            Clients.Clients(connectionIds).AddNewChatMessage(messageRow, fromUserId, toUserId, fromUserName, fromUserProfilePic, toUserName, toUserProfilePic);
        }
        public void SendUserTypingStatus(int toUserID, int fromUserID)
        {
            List<string> connectionIds = _operationRepo.GetUserConnectionID(new int[] { toUserID });
            if (connectionIds.Count > 0)
            {
                Clients.Clients(connectionIds).UserIsTyping(fromUserID);
            }
        }
        public void UpdateMessageStatus(int messageID, int currentUserID, int fromUserID)
        {
            if (messageID > 0)
            {
                _operationRepo.UpdateMessageStatusByMessageID(messageID);
            }
            else
            {
                _operationRepo.UpdateMessageStatusByUserID(fromUserID, currentUserID);
            }
            List<string> connectionIds = _operationRepo.GetUserConnectionID(new int[] { currentUserID, fromUserID });
            Clients.Clients(connectionIds).UpdateMessageStatusInChatWindow(messageID, currentUserID, fromUserID);
        }
    }
}