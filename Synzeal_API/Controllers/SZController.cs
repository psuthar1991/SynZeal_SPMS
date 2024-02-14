using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiAspNetCore.Models;
using SampleWebApiAspNetCore.Repositories;
using Synzeal_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleWebApiAspNetCore.Helpers;
using System.Text.Json;
using Synzeal_API.Dtos;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;
using System.IO;
using System.Text;
using System.Net;

namespace Synzeal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SZController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IUrlHelper _urlHelper;
        private readonly IMapper _mapper;
        public SZController(
            IUrlHelper urlHelper,
            IFoodRepository foodRepository,
            IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
            _urlHelper = urlHelper;
        }

        [Route("TestData")]
        [HttpGet]
        public ActionResult TestData()
        {
            return Ok(new
            {
                value = "Test"
            });
        }

        [Microsoft.AspNetCore.Mvc.HttpGet(Name = nameof(GetAllCompany))]
        public ActionResult GetAllCompany([FromQuery] QueryParameters queryParameters)
        {
            var items = _foodRepository.GetAllCompany(queryParameters);
            List<SZ_CompanyListDto> model = _mapper.Map<List<SZ_CompanyListDto>>(items);
            var allItemCount = model.Count();

            var paginationMetadata = new
            {
                totalCount = allItemCount,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(allItemCount)
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(new
            {
                value = model
            });
        }

        [Route("getPreviousInfoFromDB")]
        [HttpGet]
        public ActionResult getPreviousInfoFromDB([FromQuery] PreviousInfoParameter queryParameters)
        {
            var items = _foodRepository.GetPreviousInfoFromDB(queryParameters.ProductName, queryParameters.casno, queryParameters.catNo, queryParameters.company, queryParameters.QuoteId, queryParameters.isApi);
            List<SZ_QuotationDetailDto> model = _mapper.Map<List<SZ_QuotationDetailDto>>(items);
            return Ok(model);
        }

        [Route("getPreviousInfoFromDBTesting")]
        [HttpGet]
        public ActionResult getPreviousInfoFromDBTesting(int productId)
        {
            var items = _foodRepository.getPreviousInfoFromDBTesting(productId);
            List<SZ_QuotationDetailDto> model = _mapper.Map<List<SZ_QuotationDetailDto>>(items);
            return Ok(model);
        }


        [Route("GetInventoryForWebsite")]
        [HttpGet]
        public async Task<ActionResult> GetInventoryForWebsite(int productId)
        {
            var items = await _foodRepository.GetInventoryForWebsite(productId);
            return Ok(items);
        }

        [Route("GetMovetoProjectData")]
        [HttpGet]
        public async Task<ActionResult> GetMovetoProjectData()
        {
            string inhouseProjectType = Convert.ToString("5");
            var outputModel = await _foodRepository.GetMovetoProjectData();

            return Ok(outputModel);
        }

        [Route("TestSync")]
        [HttpGet]
        public async Task<ActionResult> TestSync()
        {
            var test = new WebClient().DownloadString("http://103.54.21.42:333/api/BATest/Get"); 

            var outputModel = await _foodRepository.GetMovetoProjectSAPData();
            var path = Path.Combine(Environment.CurrentDirectory, @"TxtFiles\" + "Log_" + DateTime.Now.ToString("yyyy-MM-dd-THHmmss") + ".txt");
            string strJson = JsonSerializer.Serialize<IList<MovetoProjectSAPModel>>(outputModel);
            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] content = new UTF8Encoding(true).GetBytes(strJson);
                fs.Write(content, 0, content.Length);
            }

            return Ok(outputModel);
        }

        [Route("GETSPMSPO")]
        [HttpGet]
        public async Task<ActionResult> GetMovetoProjectSAPData()
        {
            var outputModel = await _foodRepository.GetMovetoProjectSAPData();

            var path = Path.Combine(Environment.CurrentDirectory, @"TxtFiles\" + "Log_"+ DateTime.Now.ToString("yyyy-MM-dd-THHmmss") + ".txt");
            string strJson = JsonSerializer.Serialize<IList<MovetoProjectSAPModel>>(outputModel);
            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] content = new UTF8Encoding(true).GetBytes(strJson);
                fs.Write(content, 0, content.Length);
            }

            return Ok(outputModel);
        }


        [Route("GetAllCategory")]
        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {
            var outputModel = await _foodRepository.GetAllCategory();

            return Ok(outputModel);
        }

        [Route("GetProductSkusByCasNo")]
        [HttpGet]
        public async Task<ActionResult> GetProductSkusByCasNo(string casno)
        {
            var outputModel = await _foodRepository.GetProductSkusByCasNo(casno);

            return Ok(outputModel);
        }

        [Route("ProductDetails")]
        [HttpGet]
        public async Task<ActionResult> ProductDetails(int productId)
        {
            var outputModel = await _foodRepository.ProductDetails(productId);
            return Ok(outputModel);
        }

        [Route("GetProductByStartSku")]
        [HttpGet]
        public async Task<ActionResult> GetProductByStartSku(string sku)
        {
            string[] skus = sku.Split('-');
            var onlyLetters = new String(skus[1].Where(Char.IsLetter).ToArray());
            string searchsku = skus[0] + "-" + skus[1].Substring(0, 4);
            var outputModel = await _foodRepository.GetProductByStartSku(searchsku);
            var model = await _foodRepository.ProductDetailByProducts(outputModel);
            return Ok(model);
        }

        [Route("QuoteDetailsbyQuoteId")]
        [HttpGet]
        public async Task<ActionResult> GetQuoteDetailsByQuoteId(int id)
        {
            var data = await _foodRepository.GetQuoteDetailsByQuoteId(id);
            return Ok(data);
        }

        [Route("PostQuoteSuccessSAP")]
        [HttpPost]
        public ActionResult<FoodDto> PostQuoteSuccessSAP([FromBody] List<PostSuccessDto> postSuccessDto)
        {
            //TODO: Use SAPPONo instead of Id
            if (postSuccessDto == null)
            {
                return BadRequest();
            }

            try
            {
                foreach (var item in postSuccessDto)
                {
                    SZ_QuotationDetail quoteDetailsData = _foodRepository.GetQuoteDetailsById(item.Id);
                    if (quoteDetailsData != null)
                    {
                        quoteDetailsData.SAPSONo = item.SAPSONo;
                        quoteDetailsData.SAPSOLId = item.SAPSOLId;
                        quoteDetailsData.IsSuccessSAP = item.IsSuccess;
                        quoteDetailsData.SAPSODocEntry = item.SAPSODocEntry;
                        //quoteDetailsData.SAPEntryDate = DateTime.Now;
                        _foodRepository.UpdateQuoteDetails(quoteDetailsData);
                    }
                }

                if (!_foodRepository.Save())
                {
                    throw new Exception("Creating a fooditem failed on save.");
                }
            }
            catch (Exception ex)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [Route("PostPOQuoteSuccessSAP")]
        [HttpGet]
        public async Task<ActionResult> PostPOQuoteSuccessSAP(string poNumber)
        {
            //TODO: Use SAPPONo instead of Id
            if (poNumber == null)
            {
                return BadRequest();
            }

            try
            {
                var quoteDetailsData = _foodRepository.GetQuoteDetailsByPONumber(poNumber);
                if (quoteDetailsData != null && quoteDetailsData.Count > 0)
                {
                    foreach (var item in quoteDetailsData)
                    {
                        item.IsSuccessSAP = true;
                        _foodRepository.UpdateQuoteDetails(item);
                    }
                }

                if (!_foodRepository.Save())
                {
                    throw new Exception("Creating a fooditem failed on save.");
                }
            }
            catch (Exception ex)
            {
                return Ok(false);
            }

            return Ok(true);
        }
    }
}
