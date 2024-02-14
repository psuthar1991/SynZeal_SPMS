using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Synzeal_Inventory.Entity;
using ChatMessage = Synzeal_Inventory.Entity.ChatMessage;
using Synzeal_Inventory.Models.Chat;
using ChatApp.Domain.Entity;
using OnlineUser = Synzeal_Inventory.Entity.OnlineUser;
using UserNotification = Synzeal_Inventory.Entity.UserNotification;
using MessageRecords = Synzeal_Inventory.Models.Chat.MessageRecords;
using Synzeal_Inventory.Models;
using Org.BouncyCastle.Crypto;
using DocumentFormat.OpenXml.Wordprocessing;
using System.EnterpriseServices.Internal;
using WebGrease.Css.Extensions;

namespace Synzeal_Inventory.Repository
{
    public class OperationRepository : IOperationRepository
    {
        public SynzealLiveEntities _context = new SynzealLiveEntities();
        public OperationRepository(SynzealLiveEntities quoteContext)
        {
            this._context = quoteContext;
        }

        public SZ_Quotation GetQuoteByID(int id)
        {
            return _context.SZ_Quotation.Find(id);
        }

        public SZ_QuotationDetail GetQuoteDetailByID(int id)
        {
            return _context.SZ_QuotationDetail.Find(id);
        }
        public List<SZ_QuotationDetail> GetQuoteDetailByQuoteID(int quoteId)
        {
            return _context.SZ_QuotationDetail.Where(x => x.QuoteId == quoteId).OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.CreatedDate).ToList();
        }
        public List<SZ_QuotationDetail> GetQuoteDetailByProductIds(List<int?> productIds)
        {
            return _context.SZ_QuotationDetail.Where(z => productIds.Contains(z.ProductId)).ToList();
        }
        public IEnumerable<SZ_QuotationDetail> GetQuoteDetailByID(List<int> ids)
        {
            return _context.SZ_QuotationDetail.Where(x => ids.Contains(x.Id));
        }
        public int QuotationWithPOCount(int? productId, int quoteId, int? CountryTypeId, List<int> filquotehidecompanyids)
        {
            return (from t2 in _context.SZ_QuotationDetail.AsNoTracking()
                    where (t2.ProductId > 0 && t2.ProductId == productId
                    && !string.IsNullOrEmpty(t2.SZ_Quotation.PONo)
                    && (t2.SZ_Quotation.IsPark == false || t2.SZ_Quotation.IsPark == null)
                    && t2.MoveToProject == true
                    && t2.QuoteId != quoteId
                    && t2.SZ_Quotation.CompanyId != 2514
                    && t2.SZ_Quotation.CompanyId != 2515
                    && !string.IsNullOrEmpty(t2.SZ_Quotation.ApprovedBy)
                    && (t2.IsQuoteHide == false || t2.IsQuoteHide == null))
                    && t2.SZ_Quotation.CompanyId.HasValue
                    && !filquotehidecompanyids.Contains(t2.SZ_Quotation.CompanyId.Value)
                    && t2.SZ_Quotation.CountryTypeId == CountryTypeId
                    select t2).Count();
        }
        public int QuotationWithoutPOCount(int? productId, int quoteId, int? CountryTypeId, List<int> filquotehidecompanyids)
        {
            return (from t2 in _context.SZ_QuotationDetail.AsNoTracking()
                    where (t2.ProductId > 0 && t2.ProductId == productId
                    && (t2.SZ_Quotation.IsPark == false || t2.SZ_Quotation.IsPark == null)
                    && t2.QuoteId != quoteId
                    && t2.SZ_Quotation.CompanyId != 2514
                    && t2.SZ_Quotation.CompanyId != 2515
                    && !string.IsNullOrEmpty(t2.SZ_Quotation.ApprovedBy)
                    && (t2.IsQuoteHide == false || t2.IsQuoteHide == null)
                    && t2.SZ_Quotation.CompanyId.HasValue
                    && !filquotehidecompanyids.Contains(t2.SZ_Quotation.CompanyId.Value))
                    && t2.SZ_Quotation.CountryTypeId == CountryTypeId
                    select t2).Count();
        }
        public int QuotationWithPOOneYearPOCount(int? productId, int quoteId, int? CountryTypeId, List<int> filquotehidecompanyids)
        {
            DateTime lastoneyeardate = DateTime.Now.AddYears(-1);

            return (from t2 in _context.SZ_QuotationDetail.AsNoTracking()
                        //where (t2.CATNo.Trim() == x.CATNo.Trim()
                    where (t2.ProductId > 0 && t2.ProductId == productId
                          && !string.IsNullOrEmpty(t2.SZ_Quotation.PONo)
                          && (t2.SZ_Quotation.IsPark == false || t2.SZ_Quotation.IsPark == null)
                          && t2.MoveToProject == true
                          && t2.QuoteId != quoteId
                          && t2.SZ_Quotation.CompanyId != 2514
                          && t2.SZ_Quotation.CompanyId != 2515
                          && !string.IsNullOrEmpty(t2.SZ_Quotation.ApprovedBy)
                          && (t2.IsQuoteHide == false || t2.IsQuoteHide == null))
                          && t2.SZ_Quotation.CompanyId.HasValue
                          && !filquotehidecompanyids.Contains(t2.SZ_Quotation.CompanyId.Value)
                          && t2.CreatedDate >= lastoneyeardate
                          && t2.SZ_Quotation.CountryTypeId == CountryTypeId
                    select t2).Count();
        }
        public int QuotationWithoutPOOneYearPOCount(int? productId, int quoteId, int? CountryTypeId, List<int> filquotehidecompanyids)
        {
            DateTime lastoneyeardate = DateTime.Now.AddYears(-1);

            return (from t2 in _context.SZ_QuotationDetail.AsNoTracking()
                    where (t2.ProductId > 0 && t2.ProductId == productId
                    && (t2.SZ_Quotation.IsPark == false || t2.SZ_Quotation.IsPark == null)
                    && t2.QuoteId != quoteId
                    && t2.SZ_Quotation.CompanyId != 2514
                    && t2.SZ_Quotation.CompanyId != 2515
                    && !string.IsNullOrEmpty(t2.SZ_Quotation.ApprovedBy)
                    && (t2.IsQuoteHide == false || t2.IsQuoteHide == null)
                    && t2.SZ_Quotation.CompanyId.HasValue
                    && !filquotehidecompanyids.Contains(t2.SZ_Quotation.CompanyId.Value))
                    && t2.CreatedDate >= lastoneyeardate
                    && t2.SZ_Quotation.CountryTypeId == CountryTypeId
                    select t2).Count();
        }

        public SZ_ChildCOA GetChildCOAById(int? id)
        {
            return _context.SZ_ChildCOA.Find(id);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ChatMessage SaveChatMessage(ChatMessage objentity)
        {
            _context.ChatMessages.Add(objentity);
            _context.SaveChanges();
            return objentity;
        }
        public MessageRecords GetChatMessagesByUserID(int currentUserID, int toUserID, int lastMessageID = 0)
        {
            MessageRecords obj = new MessageRecords();
            var messages = _context.ChatMessages.Where(m => m.IsActive == true && (m.ToUserID == toUserID || m.FromUserID == toUserID) && (m.ToUserID == currentUserID || m.FromUserID == currentUserID)).OrderByDescending(m => m.CreatedOn);
            if (lastMessageID > 0)
            {
                obj.Messages = messages.Where(m => m.ChatMessageID < lastMessageID).Take(20).ToList().OrderBy(m => m.CreatedOn).ToList();
            }
            else
            {
                obj.Messages = messages.Take(20).ToList().OrderBy(m => m.CreatedOn).ToList();
            }
            obj.LastChatMessageId = obj.Messages.OrderBy(m => m.ChatMessageID).Select(m => m.ChatMessageID).FirstOrDefault();
            return obj;
        }
        public void UpdateMessageStatusByUserID(int fromUserID, int currentUserID)
        {
            var unreadMessages = _context.ChatMessages.Where(m => m.Status == "Sent" && m.ToUserID == currentUserID && m.FromUserID == fromUserID && m.IsActive == true).ToList();
            unreadMessages.ForEach(m =>
            {
                m.Status = "Viewed";
                m.ViewedOn = System.DateTime.Now;
            });
            _context.SaveChanges();
        }
        public void UpdateMessageStatusByMessageID(int messageID)
        {
            var unreadMessages = _context.ChatMessages.Where(m => m.ChatMessageID == messageID).FirstOrDefault();
            if (unreadMessages != null)
            {
                unreadMessages.Status = "Viewed";
                unreadMessages.ViewedOn = System.DateTime.Now;
                _context.SaveChanges();
            }
        }
        public int[] GetFriendUserIds(int userID)
        {
            var arr = _context.Customers.Where(m => (m.Email.Contains("@synzeal.com") || m.Email.Contains("parthsuthar2010")) && m.Active == true && m.Deleted == false).Select(m => m.Id).ToArray();
            return arr;
        }
        public List<OnlineUserDetails> GetRecentChats(int currentUserID)
        {
            int[] friends = GetFriendUserIds(currentUserID);
            var recentMessages = _context.ChatMessages.Where(m => m.IsActive == true && (m.ToUserID == currentUserID || m.FromUserID == currentUserID)).OrderByDescending(m => m.CreatedOn).ToList();
            var userIds = recentMessages.Select(m => (m.ToUserID == currentUserID ? m.FromUserID : m.ToUserID)).Distinct().ToArray();
            var userIdsList = userIds.ToList();
            var messagesByUserId = recentMessages.Where(m => m.ToUserID == currentUserID && m.Status == "Sent").ToList();
            var newMessagesCount = (from p in messagesByUserId
                                    group p by p.FromUserID into g
                                    select new { FromUserID = g.Key, Count = g.Count() }).ToList();
            var onlineUserIDs = _context.OnlineUsers.Where(m => friends.Contains(m.UserID) && userIds.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserID).ToArray();
            var users = (from m in _context.Customers
                         join v in userIdsList on m.Id equals v
                         select new OnlineUserDetails
                         {
                             UserID = m.Id,
                             Name = "",
                             //ProfilePicture = m.ProfilePicture,
                             //Gender = m.Gender,
                             IsOnline = onlineUserIDs.Contains(m.Id) ? true : false
                         }).ToList();
            if (users != null && users.Count > 0)
            {
                List<int> cids = users.Select(x => x.UserID).ToList();
                var customs = _context.Customers.Where(x => cids.Contains(x.Id)).ToList();
                users.ForEach(m =>
                {
                    var cus = customs.Where(x => x.Id == m.UserID).FirstOrDefault();
                    m.Name = cus.GetFullName(_context);
                    m.UnReadMessageCount = newMessagesCount.Where(x => x.FromUserID == m.UserID).Select(x => x.Count).FirstOrDefault();
                    m.LastUpdationTime = recentMessages.Select(x => x.CreatedOn).LastOrDefault();
                    m.ProfilePicture = recentMessages.Select(x => x.Message).LastOrDefault();
                });
            }

            users = users.OrderBy(d => userIdsList.IndexOf(d.UserID)).ToList();
            return users;
        }
        public OnlineUserDetails GetUserOnlineStatus(int userID)
        {
            OnlineUserDetails obj = new OnlineUserDetails();
            obj.UserID = userID;
            var objList = _context.OnlineUsers.Where(m => m.UserID == userID && m.IsActive == true).ToList();
            if (objList != null && objList.Count > 0)
            {
                obj.IsOnline = false;
                var onlineConnections = objList.Where(m => m.IsOnline).ToList();
                var offlineConnections = objList.Where(m => !m.IsOnline).ToList();
                if (onlineConnections != null && onlineConnections.Count > 0)
                {
                    obj.IsOnline = true;
                }
                else if (offlineConnections != null && offlineConnections.Count > 0)
                {
                    obj.IsOnline = false;
                    obj.LastUpdationTime = offlineConnections.OrderByDescending(m => m.UpdatedOn).Select(m => m.UpdatedOn).FirstOrDefault();
                }
            }
            return obj;
        }

        public List<Models.Chat.UserNotificationList> GetUserNotifications(int toUserID)
        {
            var listQuery = (from u in _context.UserNotifications
                             join v in _context.Customers on u.FromUserID equals v.Id
                             where u.ToUserID == toUserID && u.IsActive == true
                             select new Models.Chat.UserNotificationList()
                             {
                                 NotificationID = u.NotificationID,
                                 NotificationType = u.NotificationType,
                                 User = v,
                                 NotificationStatus = u.Status,
                                 CreatedOn = u.CreatedOn
                             }).OrderByDescending(m => m.CreatedOn);
            int totalNotifications = listQuery.Count();
            var list = listQuery.Take(3).ToList();
            list.ForEach(m => m.TotalNotifications = totalNotifications);
            return list;
        }
        public int GetUserNotificationCounts(int toUserID)
        {
            int count = _context.UserNotifications.Where(m => m.Status == "New" && m.ToUserID == toUserID && m.IsActive == true).Count();
            return count;
        }
        public void ChangeNotificationStatus(int[] notificationIDs)
        {
            _context.UserNotifications.Where(m => notificationIDs.Contains(m.NotificationID)).ToList().ForEach(m => m.Status = "Read");
            _context.SaveChanges();
        }

        public int SaveUserNotification(string notificationType, int fromUserID, int toUserID)
        {
            UserNotification notification = new UserNotification();
            notification.CreatedOn = System.DateTime.Now;
            notification.IsActive = true;
            notification.NotificationType = notificationType;
            notification.FromUserID = fromUserID;
            notification.Status = "New";
            notification.UpdatedOn = System.DateTime.Now;
            notification.ToUserID = toUserID;
            _context.UserNotifications.Add(notification);
            _context.SaveChanges();
            return notification.NotificationID;
        }

        public void SaveUserOnlineStatus(OnlineUser objentity)
        {
            var obj = _context.OnlineUsers.Where(m => m.UserID == objentity.UserID && m.IsActive == true && m.ConnectionID == objentity.ConnectionID).FirstOrDefault();
            if (obj != null)
            {
                obj.IsOnline = objentity.IsOnline;
                obj.UpdatedOn = System.DateTime.Now;
                obj.ConnectionID = objentity.ConnectionID;
            }
            else
            {
                objentity.CreatedOn = System.DateTime.Now;
                objentity.UpdatedOn = System.DateTime.Now;
                objentity.IsActive = true;
                _context.OnlineUsers.Add(objentity);
            }
            _context.SaveChanges();
        }
        public List<string> GetUserConnectionID(int UserID)
        {
            var obj = _context.OnlineUsers.Where(m => m.UserID == UserID && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionID).ToList();
            return obj;
        }
        public List<string> GetUserConnectionID(int[] userIDs)
        {
            var obj = _context.OnlineUsers.Where(m => userIDs.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionID).ToList();
            return obj;
        }

        public List<OnlineUserDetails> GetOnlineFriends(int userID)
        {
            int[] friends = GetFriendUserIds(userID);
            var friendOnlineDetails = _context.OnlineUsers.Where(m => friends.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).ToList();
            var obj = (from v in _context.Customers
                       where friends.Contains(v.Id)
                       select new OnlineUserDetails
                       {
                           UserID = v.Id,
                           Name = "",
                           //ProfilePicture = v.ProfilePicture,
                           //Gender = v.Gender
                       }).OrderBy(m => m.Name).ToList();
            if (obj != null && obj.Count > 0)
            {
                List<int> cids = obj.Select(x => x.UserID).ToList();
                var customs = _context.Customers.Where(x => cids.Contains(x.Id)).ToList();
                obj.ForEach(item =>
                {
                    var cus = customs.Where(x => x.Id == item.UserID).FirstOrDefault();
                    item.Name = cus.GetFullName(_context);
                });
            }

            var onlineUserIds = friendOnlineDetails.Select(m => m.UserID).ToArray();
            //obj = obj.Where(m => onlineUserIds.Contains(m.UserID)).ToList();
            obj.ForEach(m =>
            {
                m.ConnectionID = friendOnlineDetails.Where(x => x.UserID == m.UserID).Select(x => x.ConnectionID).ToList();
            });
            return obj;
        }

        public List<OnlineUserDetails> GetFriends(int userID)
        {
            var friendIds = GetFriendUserIds(userID);
            var onlineUserIDs = _context.OnlineUsers.Where(m => friendIds.Contains(m.UserID) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserID).ToArray();
            var users = _context.Customers.Where(m => friendIds.Contains(m.Id)).Select(m => new OnlineUserDetails
            {
                UserID = m.Id,
                Name = m.GetFullName(_context),
                //ProfilePicture = m.ProfilePicture,
                //Gender = m.Gender,
                IsOnline = onlineUserIDs.Contains(m.Id) ? true : false
            }).ToList();
            return users;
        }

        public List<SZ_MasterCOA> LoadMasterCOAData()
        {
            return _context.SZ_MasterCOA.ToList();
        }
        public List<SZ_MasterCOA> LoadMasterCOAData(List<int?> productIds)
        {
            return _context.SZ_MasterCOA.Where(x=> productIds.Contains(x.ProductId)).ToList();
        }
        public List<SZ_ChildCOA> LoadChildCOAData()
        {
            return _context.SZ_ChildCOA.ToList();
        }
        public List<SZ_ChildCOA>  LoadChildCOAData(List<int> masterCoaIds)
        {
            return _context.SZ_ChildCOA.Where(x => x.MasterCOAID.HasValue && masterCoaIds.Contains(x.MasterCOAID.Value)).ToList();
        }
        public List<Product> LoadProductData()
        {
            return _context.Products.Where(x => x.Published == true && x.Deleted == false).ToList();
        }
        public List<Product> LoadProductData(List<int?> productIds)
        {
            return _context.Products.Where(x => productIds.Contains(x.Id) && x.Published == true && x.Deleted == false).ToList();
        }
        public List<SZ_Inventory> LoadInventoryData()
        {
            return _context.SZ_Inventory.ToList();
        }
        public List<SZ_Inventory> LoadInventoryData(List<int?> productIds)
        {
            return _context.SZ_Inventory.Where(x => productIds.Contains(x.ProductId)).ToList();
        }
        public List<Synzeal_Inventory.Entity.Category> LoadCategoryData()
        {
            return _context.Categories.Where(x => x.ParentCategoryId == 0 && x.Deleted == false && x.Published).ToList();

        }
        public List<SZ_CompanyList> LoadCompanyData()
        {
            return _context.SZ_CompanyList.ToList();

        }
        public IQueryable<SZ_QuotationDetail> LoadProjectData()
        {
            string inhouseProjectType = Convert.ToString((int)EnumList.ProjectType.InHouse);

            return (from i in _context.SZ_Quotation.AsNoTracking()
             join t2 in _context.SZ_QuotationDetail.AsNoTracking() on i.Id equals t2.QuoteId
             where t2.MoveToProject == true
             && string.IsNullOrEmpty(t2.TrackingNo)
             && (t2.IsOnHold == false || t2.IsOnHold == null)
             && (t2.ProjectType != inhouseProjectType || t2.ProjectType == null)
             && i.CompanyId != 208
             orderby t2.MoveProjectDate descending
             select t2).Distinct().OrderByDescending(x => x.MoveProjectDate).ThenBy(x => x.SrPo).AsQueryable();
        }
        public IQueryable<SZ_QuotationDetail> LoadProjectWatchListData()
        {
            string inhouseProjectType = Convert.ToString((int)EnumList.ProjectType.InHouse);
            var list = (from i in _context.SZ_Quotation.AsNoTracking()
                    join t2 in _context.SZ_QuotationDetail.AsNoTracking() on i.Id equals t2.QuoteId
                    where t2.MoveToProject == true
                    && string.IsNullOrEmpty(t2.TrackingNo)
                    && (t2.IsOnHold == false || t2.IsOnHold == null)
                    && (t2.ProjectType != inhouseProjectType || t2.ProjectType == null)
                    && i.CompanyId != 208
                    //&& (i.SZ_CompanyList.IsConversionReport == true || (t2.ProStatus == "16" || t2.ProStatus == "17"))
                    orderby t2.MoveProjectDate descending
                    select t2).Distinct().OrderByDescending(x => x.MoveProjectDate).ThenBy(x => x.SrPo).AsQueryable();
            
            return list.Where(x => (x.WatchListCount == 1 && x.SZ_Quotation.SZ_CompanyList.IsConversionReport == true) || (x.ProStatus == "16" || x.ProStatus == "17" || x.ProStatus == "47")).AsQueryable();
        }

        public int LoadProjectWatchListDataCount()
        {
            string inhouseProjectType = Convert.ToString((int)EnumList.ProjectType.InHouse);
            var list = (from i in _context.SZ_Quotation.AsNoTracking()
                        join t2 in _context.SZ_QuotationDetail.AsNoTracking() on i.Id equals t2.QuoteId
                        where t2.MoveToProject == true
                        && string.IsNullOrEmpty(t2.TrackingNo)
                        && (t2.IsOnHold == false || t2.IsOnHold == null)
                        && (t2.ProjectType != inhouseProjectType || t2.ProjectType == null)
                        && i.CompanyId != 208
                        //&& (i.SZ_CompanyList.IsConversionReport == true || (t2.ProStatus == "16" || t2.ProStatus == "17"))
                        orderby t2.MoveProjectDate descending
                        select t2).Distinct().OrderByDescending(x => x.MoveProjectDate).ThenBy(x => x.SrPo).AsQueryable();

            return list.Where(x => (x.WatchListCount == 1 && x.SZ_Quotation.SZ_CompanyList.IsConversionReport == true) || (x.ProStatus == "16" || x.ProStatus == "17" || x.ProStatus == "47")).Count();
        }
    }
}