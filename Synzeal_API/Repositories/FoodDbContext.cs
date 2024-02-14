using Microsoft.EntityFrameworkCore;
using SampleWebApiAspNetCore.Entities;
using Synzeal_API.Entities;

namespace SampleWebApiAspNetCore.Repositories
{
    public class SZDbContext : DbContext
    {
        public SZDbContext(DbContextOptions<SZDbContext> options)
           : base(options)
        {

        }

        public DbSet<FoodEntity> FoodItems { get; set; }
        public DbSet<GenericAttribute> GenericAttribute { get; set; }
        
        public DbSet<SZ_CompanyList> SZ_CompanyList { get; set; }
        public DbSet<SZ_ClubQuote> SZ_ClubQuote { get; set; }
        public DbSet<SZ_Quotation> SZ_Quotation { get; set; }
        public DbSet<SZ_QuotationDetail> SZ_QuotationDetail { get; set; }
        public DbSet<SZ_QuoteDetailForm> SZ_QuoteDetailForm { get; set; }
        public DbSet<SZ_QuoteDetailsComment> SZ_QuoteDetailsComment { get; set; }
        public DbSet<SZ_Inventory> SZ_Inventory { get; set; }

        public DbSet<SZ_MasterCOA> SZ_MasterCOA { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Picture> Picture { get; set; }

        public DbSet<ProductPicture> ProductPicture { get; set; }
    }
}
