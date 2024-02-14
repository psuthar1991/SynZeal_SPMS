using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Models;
using Synzeal_API.Dtos;
using Synzeal_API.Entities;

namespace SampleWebApiAspNetCore.Repositories
{
    public interface IFoodRepository
    {
        FoodEntity GetSingle(int id);
        void Add(FoodEntity item);
        void Delete(int id);
        FoodEntity Update(int id, FoodEntity item);
        IQueryable<FoodEntity> GetAll(QueryParameters queryParameters);

        IQueryable<SZ_CompanyList> GetAllCompany(QueryParameters queryParameters);

        IList<SZ_QuotationDetail> GetPreviousInfoFromDB(string ProductName, string casno, string catNo, string company, int QuoteId = 0, bool isApi = false);

        IList<SZ_QuotationDetail> getPreviousInfoFromDBTesting(int productId);

         Task<IList<SZ_InventoryDto>> GetInventoryForWebsite(int productId);

        Task<IList<MovetoProjectModel>> GetMovetoProjectData();

        Task<IList<MovetoProjectSAPModel>> GetMovetoProjectSAPData();

        Task<IList<Category>> GetAllCategory();

        Task<IList<Product>> GetProductSkusByCasNo(string casno);

        ICollection<FoodEntity> GetRandomMeal();

        Task<ProductDetailsDto> ProductDetails(int productId);
        Task<IList<Product>> GetProductByStartSku(string sku);
        Task<List<ProductDetailsDto>> ProductDetailByProducts(IList<Product> products);
        Task<IQueryable<SZ_QuotationDetail>> GetQuoteDetailsByQuoteId(int Id);
        SZ_QuotationDetail GetQuoteDetailsById(int Id);
        SZ_QuotationDetail UpdateQuoteDetails(SZ_QuotationDetail quotationDetails);
        //Task<IQueryable<SZ_Quotation>> GetQuoteByPONumber(string poNumber);
        List<SZ_QuotationDetail> GetQuoteDetailsByPONumber(string poNumber);



        int Count();

        bool Save();
    }
}
