using ChatApp.Domain.Entity;
using Synzeal_Inventory.Entity;
using Synzeal_Inventory.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatMessage = Synzeal_Inventory.Entity.ChatMessage;
using OnlineUser = Synzeal_Inventory.Entity.OnlineUser;

namespace Synzeal_Inventory.Repository
{
    public interface IOperationRepository : IDisposable
    {
        SZ_Quotation GetQuoteByID(int id);
        SZ_QuotationDetail GetQuoteDetailByID(int id);
        List<SZ_QuotationDetail> GetQuoteDetailByQuoteID(int quoteId);
        List<SZ_QuotationDetail> GetQuoteDetailByProductIds(List<int?> productIds);
        IEnumerable<SZ_QuotationDetail> GetQuoteDetailByID(List<int> ids);
        int QuotationWithPOCount(int? productId, int quoteId,int? CountryTypeId, List<int> filquotehidecompanyids);
        int QuotationWithoutPOCount(int? productId, int quoteId, int? CountryTypeId, List<int> filquotehidecompanyids);
        int QuotationWithPOOneYearPOCount(int? productId, int quoteId, int? CountryTypeId, List<int> filquotehidecompanyids);
        int QuotationWithoutPOOneYearPOCount(int? productId, int quoteId, int? CountryTypeId, List<int> filquotehidecompanyids);
            
        SZ_ChildCOA GetChildCOAById(int? id);

        ChatMessage SaveChatMessage(ChatMessage objentity);
        Models.Chat.MessageRecords GetChatMessagesByUserID(int currentUserID, int toUserID, int lastMessageID = 0);
        void UpdateMessageStatusByUserID(int fromUserID, int currentUserID);
        void UpdateMessageStatusByMessageID(int messageID);


        List<OnlineUserDetails> GetRecentChats(int currentUserID);
        OnlineUserDetails GetUserOnlineStatus(int userID);
        List<Models.Chat.UserNotificationList> GetUserNotifications(int toUserID);
        int GetUserNotificationCounts(int toUserID);
        void ChangeNotificationStatus(int[] notificationIDs);
        int SaveUserNotification(string notificationType, int fromUserID, int toUserID);
        void SaveUserOnlineStatus(OnlineUser objentity);
        List<string> GetUserConnectionID(int UserID);
        List<string> GetUserConnectionID(int[] userIDs);
        List<OnlineUserDetails> GetOnlineFriends(int userID);
        List<OnlineUserDetails> GetFriends(int userID);

        List<SZ_MasterCOA> LoadMasterCOAData();
        List<SZ_MasterCOA> LoadMasterCOAData(List<int?> productIds);
        List<SZ_ChildCOA> LoadChildCOAData();
        List<SZ_ChildCOA> LoadChildCOAData(List<int> masterCoaIds); 
        List<Product> LoadProductData();
        List<Product> LoadProductData(List<int?> productIds);
        List<SZ_Inventory> LoadInventoryData();
        List<SZ_Inventory> LoadInventoryData(List<int?> productIds);
        List<Category> LoadCategoryData();
        List<SZ_CompanyList> LoadCompanyData();

        IQueryable<SZ_QuotationDetail> LoadProjectData();

        IQueryable<SZ_QuotationDetail> LoadProjectWatchListData();
        int LoadProjectWatchListDataCount();
    }
}