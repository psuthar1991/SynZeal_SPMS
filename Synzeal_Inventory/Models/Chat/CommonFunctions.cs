using ChatApp.Domain.Entity;
using MailChimp.Net.Models;
using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using ChatMessage = Synzeal_Inventory.Entity.ChatMessage;

namespace Synzeal_Inventory.Models.Chat
{
    public static class CommonFunctions
    {
        public static SynzealLiveEntities _db = new SynzealLiveEntities();
        public static string GetProfilePicture(string profilePicture, string gender)
        {
            string profilePicturePath = "";
            if (string.IsNullOrEmpty(profilePicture))
            {
                if (gender == "Female")
                {
                    profilePicturePath = "/Content/Images/female-default-pic.jpg";
                }
                else
                {
                    profilePicturePath = "/Content/Images/male-default-pic.jpg";
                }
            }
            else
            {
                profilePicturePath = profilePicture;
            }
            return profilePicturePath;
        }
        public static UserModel GetUserModel(int id, Entity.Customer objentity = null, string friendRequestStatus = "", bool isRequestReceived = false)
        {
            var user = new Entity.Customer();
            if (objentity != null)
            {
                user = objentity;
            }
            else
            {
                user = _db.Customers.Where(x=> x.Id == id).FirstOrDefault();
            }
            UserModel objmodel = new UserModel();
            if (user != null)
            {
                objmodel.IsRequestReceived = isRequestReceived;
                objmodel.FriendRequestStatus = friendRequestStatus;
                objmodel.UserID = user.Id;
                objmodel.Name = user.GetFullName();
                //objmodel.Gender = user.Gender;
                //objmodel.DOB = user.DOB.ToShortDateString();
                //if (user.DOB != null)
                //{
                //    objmodel.Age = Convert.ToString(Math.Floor(DateTime.Now.Subtract(Convert.ToDateTime(user.DOB)).TotalDays / 365.0)) + " Years";
                //}
                //else
                //{
                //    objmodel.Age = "NaN";
                //}
                //objmodel.Bio = user.Bio;
            }
            return objmodel;
        }
        public static MessageModel GetMessageModel(ChatMessage objentity)
        {
            MessageModel objmodel = new MessageModel();
            objmodel.ChatMessageID = objentity.ChatMessageID;
            objmodel.FromUserID = objentity.FromUserID;
            objmodel.ToUserID = objentity.ToUserID;
            objmodel.Message = objentity.Message;
            objmodel.Status = objentity.Status;
            objmodel.CreatedOn = Convert.ToString(objentity.CreatedOn);
            objmodel.UpdatedOn = Convert.ToString(objentity.UpdatedOn);
            objmodel.ViewedOn = Convert.ToString(objentity.ViewedOn);
            objmodel.IsActive = objentity.IsActive;
            return objmodel;
        }
    }
}