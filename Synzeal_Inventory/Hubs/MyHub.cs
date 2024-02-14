using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Synzeal_Inventory.Entity;
using Synzeal_Inventory.Models;

namespace Synzeal_Inventory.Controllers
{
    public class MyHub : Hub
    {
        static List<UserInfo> UsersList = new List<UserInfo>();
        static List<MessageInfo> MessageList = new List<MessageInfo>();
        static SynzealLiveEntities db = new SynzealLiveEntities();


        public void Loadusers()
        {

        }

        //-->>>>> ***** Receive Request From Client [  Connect  ] *****
        public void Connect(string userName)
        {
            var userid = Convert.ToInt32(userName);
            userName = SessionCookieManagement.UserName;
            var id = Context.ConnectionId;
            string userGroup = "";
            //Manage Hub Class
            //if freeflag==0 ==> Busy
            //if freeflag==1 ==> Free

            //if tpflag==0 ==> User
            //if tpflag==1 ==> Admin


            //var ctx = new Synzeal_Inventory.Entity();

            //var userInfo =
            //     (from m in ctx.cust
            //      where m.UserName == userName && m.Password == password
            //      select new { m.UserID, m.UserName, m.AdminCode }).FirstOrDefault();

            try
            {
                    if (UsersList.Count(x => x.UserID == userid) == 0)
                {
                    //if (SessionCookieManagement.IsAdmin == true)
                    //{
                    //    //now we encounter ordinary user which needs userGroup and at this step, system assigns the first of free Admin among UsersList
                    //    var strg = (from s in UsersList where (s.tpflag == "1") && (s.freeflag == "1") select s).First();
                    //    userGroup = strg.UserGroup;

                    //    //Admin becomes busy so we assign zero to freeflag which is shown admin is busy
                    //    strg.freeflag = "0";

                    //    //now add USER to UsersList
                    //    UsersList.Add(new UserInfo
                    //    {
                    //        ConnectionId = id,
                    //        AdminID = SessionCookieManagement.UserId,
                    //        UserID = SessionCookieManagement.UserId,
                    //        UserName = userName,
                    //        UserGroup = userGroup,
                    //        freeflag = "0",
                    //        tpflag = "0",
                    //    });
                    //    //whether it is Admin or User now both of them has userGroup and I Join this user or admin to specific group 
                    //    Groups.Add(Context.ConnectionId, userGroup);
                    //    Clients.Caller.onConnected(id, userName, SessionCookieManagement.UserId, userGroup);

                    //}
                    //else
                    //{
                        //If user has admin code so admin code is same userGroup
                        //now add ADMIN to UsersList
                        UsersList.Add(new UserInfo
                        {
                            ConnectionId = id,
                            UserID = SessionCookieManagement.UserId,
                            UserName = userName,
                            UserGroup = SessionCookieManagement.UserId.ToString(),
                            freeflag = "1",
                            tpflag = "1"
                        });
                        //whether it is Admin or User now both of them has userGroup and I Join this user or admin to specific group 
                        Groups.Add(Context.ConnectionId, SessionCookieManagement.UserId.ToString());
                        Clients.Caller.onConnected(id, userName, SessionCookieManagement.UserId, SessionCookieManagement.UserId.ToString());

                    //}
                }
            }

            catch
            {
                string msg = "All Administrators are busy, please be patient and try again";
                //***** Return to Client *****
                Clients.Caller.NoExistAdmin();
            }
        }
        // <<<<<-- ***** Return to Client [  NoExist  ] *****

        public List<UserInfo> GetUserList(int loginUserId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            var clients = context.Clients.All;

            var users = (from i in db.Customers
                         where (i.Email.Contains("@synzeal.com") || i.Email.Contains("parthsuthar2010")) && i.Active == true && i.Deleted == false
                         select i).ToList();

            foreach (var item in users)
            {
                UsersList.Add(new UserInfo
                {
                    ConnectionId = item.CustomerGuid.ToString(),
                    UserID = item.Id,
                    UserName = item.Email,
                    UserGroup = item.Id.ToString(),
                    freeflag = "1",
                    tpflag = "1"
                });
            }

            

            return UsersList;
        }


        //--group ***** Receive Request From Client [  SendMessageToGroup  ] *****
        public void SendMessageToGroup(string userName, string message, string connectionId)
        {
            int uID = Convert.ToInt32(userName);
            if (UsersList.Count != 0)
            {
                var strg = (from s in UsersList where (s.UserID == uID) select s).First();
                MessageList.Add(new MessageInfo { UserName = userName, Message = message, UserGroup = strg.UserGroup });
                string strgroup = strg.UserGroup;
                // If you want to Broadcast message to all UsersList use below line
                Clients.All.getMessages(userName, message);

                //If you want to establish peer to peer connection use below line so message will be send just for user and admin who are in same group
                //***** Return to Client *****
                 Clients.Group(strgroup).getMessages(userName, message);

                Clients.Client(connectionId).addChatMessage(userName, message);
                Clients.Client(connectionId).getMessages(userName, message);
            }

        }
        // <<<<<-- ***** Return to Client [  getMessages  ] *****


        //--group ***** Receive Request From Client ***** { Whenever User close session then OnDisconneced will be occurs }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopcall = true)
        {

            var item = UsersList.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                UsersList.Remove(item);

                var id = Context.ConnectionId;

                if (item.tpflag == "0")
                {
                    //user logged off == user
                    try
                    {
                        var stradmin = (from s in UsersList where (s.UserGroup == item.UserGroup) && (s.tpflag == "1") select s).First();
                        //become free
                        stradmin.freeflag = "1";
                    }
                    catch
                    {
                        //***** Return to Client *****
                        Clients.Caller.NoExistAdmin();
                    }

                }

                //save conversation to dat abase


            }

            return base.OnDisconnected(true);
        }
    }
}