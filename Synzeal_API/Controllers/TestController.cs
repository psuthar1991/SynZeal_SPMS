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
using Microsoft.AspNetCore.Authorization;
using Synzeal_API.Models;

namespace Synzeal_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IUrlHelper _urlHelper;
        private readonly IMapper _mapper;
        public TestController(
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

        [Route("Sync")]
        [HttpGet]
        public ActionResult Sync()
        {
            return Ok(new
            {
                value = "Test"
            });
        }

    }
}
