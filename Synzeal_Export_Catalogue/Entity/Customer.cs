//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_Export_Catalogue.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public Customer()
        {
            this.ActivityLogs = new HashSet<ActivityLog>();
            this.BackInStockSubscriptions = new HashSet<BackInStockSubscription>();
            this.BlogComments = new HashSet<BlogComment>();
            this.ExternalAuthenticationRecords = new HashSet<ExternalAuthenticationRecord>();
            this.SZ_UserManagement = new HashSet<SZ_UserManagement>();
            this.Forums_Post = new HashSet<Forums_Post>();
            this.Forums_Subscription = new HashSet<Forums_Subscription>();
            this.Forums_Topic = new HashSet<Forums_Topic>();
            this.Logs = new HashSet<Log>();
            this.NewsComments = new HashSet<NewsComment>();
            this.Orders = new HashSet<Order>();
            this.PollVotingRecords = new HashSet<PollVotingRecord>();
            this.Forums_PrivateMessage = new HashSet<Forums_PrivateMessage>();
            this.Forums_PrivateMessage1 = new HashSet<Forums_PrivateMessage>();
            this.ProductReviews = new HashSet<ProductReview>();
            this.ReturnRequests = new HashSet<ReturnRequest>();
            this.RewardPointsHistories = new HashSet<RewardPointsHistory>();
            this.ShoppingCartItems = new HashSet<ShoppingCartItem>();
            this.CustomerRoles = new HashSet<CustomerRole>();
            this.Addresses = new HashSet<Address>();
        }
    
        public int Id { get; set; }
        public System.Guid CustomerGuid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSalt { get; set; }
        public string AdminComment { get; set; }
        public bool IsTaxExempt { get; set; }
        public int AffiliateId { get; set; }
        public int VendorId { get; set; }
        public bool HasShoppingCartItems { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool IsSystemAccount { get; set; }
        public string SystemName { get; set; }
        public string LastIpAddress { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> LastLoginDateUtc { get; set; }
        public System.DateTime LastActivityDateUtc { get; set; }
        public Nullable<int> BillingAddress_Id { get; set; }
        public Nullable<int> ShippingAddress_Id { get; set; }
        public string RealPassword { get; set; }
        public string SecEmail { get; set; }
        public string SecMobile { get; set; }
        public string OTP { get; set; }
        public Nullable<System.DateTime> OTPValidationDate { get; set; }
    
        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
        public virtual Address Address { get; set; }
        public virtual Address Address1 { get; set; }
        public virtual ICollection<BackInStockSubscription> BackInStockSubscriptions { get; set; }
        public virtual ICollection<BlogComment> BlogComments { get; set; }
        public virtual ICollection<ExternalAuthenticationRecord> ExternalAuthenticationRecords { get; set; }
        public virtual ICollection<SZ_UserManagement> SZ_UserManagement { get; set; }
        public virtual ICollection<Forums_Post> Forums_Post { get; set; }
        public virtual ICollection<Forums_Subscription> Forums_Subscription { get; set; }
        public virtual ICollection<Forums_Topic> Forums_Topic { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<NewsComment> NewsComments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PollVotingRecord> PollVotingRecords { get; set; }
        public virtual ICollection<Forums_PrivateMessage> Forums_PrivateMessage { get; set; }
        public virtual ICollection<Forums_PrivateMessage> Forums_PrivateMessage1 { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<ReturnRequest> ReturnRequests { get; set; }
        public virtual ICollection<RewardPointsHistory> RewardPointsHistories { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual ICollection<CustomerRole> CustomerRoles { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
