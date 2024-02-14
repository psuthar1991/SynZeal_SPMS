using ChatApp.Domain.Abstract;
using Synzeal_Inventory.Entity;
using Synzeal_Inventory.Models;
using Synzeal_Inventory.Models.Chat;
using Synzeal_Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        public static SynzealLiveEntities db = new SynzealLiveEntities();
        private IOperationRepository _operationRepo;
        public ChatController()
        {
            this._operationRepo = new OperationRepository(db);
        }

        public ActionResult _Messages(int Id)
        {
            var userModel = CommonFunctions.GetUserModel(Id);
            var messages = _operationRepo.GetChatMessagesByUserID(SessionCookieManagement.UserId, Id);
            var objmodel = new ChatMessageModel();
            objmodel.UserDetail = userModel;
            objmodel.ChatMessages = messages.Messages.Select(m => CommonFunctions.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            var onlineStatus = _operationRepo.GetUserOnlineStatus(Id);
            if (onlineStatus != null)
            {
                objmodel.IsOnline = onlineStatus.IsOnline;
                objmodel.LastSeen = Convert.ToString(onlineStatus.LastUpdationTime);
            }
            return View(objmodel);
        }
        public ActionResult GetRecentMessages(int Id, int lastChatMessageId)
        {
            var messages = _operationRepo.GetChatMessagesByUserID(SessionCookieManagement.UserId, Id, lastChatMessageId);
            var objmodel = new ChatMessageModel();
            objmodel.ChatMessages = messages.Messages.Select(m => CommonFunctions.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            return Json(objmodel, JsonRequestBehavior.AllowGet);
        }
    }
}
