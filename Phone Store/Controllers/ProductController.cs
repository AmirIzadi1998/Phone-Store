using Application.Repositories.ProductRepo;
using Core.Context;
using Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Phone_Store.Controllers
{
    [Route("api/Product")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IPhone _phone;
        private readonly ILogger<ProductController> _logger;


        public ProductController(IPhone phone, ILogger<ProductController> logger)
        {
            _phone = phone ?? throw new ArgumentNullException(nameof(phone));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region GetAll

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var product = await _phone.GetAll();
            if (product == null)
            {
                _logger.LogInformation("No data available");
                return NotFound();
            }

            return Ok(product);
        }

        #endregion

        #region GetById

        [HttpGet("{GetId}")]
        public async Task<IActionResult> GetById(int GetId)
        {
            var product = await _phone.GetById(GetId);
            if (product == null)
            {
                _logger.LogInformation("No data available");
                return NotFound();
            }

            return Ok(product);
        }

        #endregion

        #region GetByName

        [HttpGet("GetName")]
        public async Task<IActionResult> GetByName(string GetName)
        {
            var product = await _phone.GetByName(GetName);
            if (product == null)
            {
                _logger.LogInformation("No data available");
                return NotFound();
            }

            return Ok(product);
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> Insert(PhoneDto phoneDto)
        {
            var product = await _phone.Insert(phoneDto);
            if (product == null)
            {
                _logger.LogError("No data available");
                return NotFound();
            }

            return Ok(product);
        }

        #endregion

        #region DeleteById

        [HttpDelete("{DeleteById}")]
        public async Task<IActionResult> DeleteById(int DeleteById)
        {
            var product = await _phone.DeleteById(DeleteById);
            if (product == null)
            {
                _logger.LogError("Error");
                return NotFound();
            }

            return Ok(product);
        }

        #endregion

        #region Update

        [HttpPut]
        public async Task<IActionResult> Put(PhoneDto phoneDto)
        {
            var product = await _phone.Update(phoneDto);
            if (product == null)
            {
                _logger.LogError("Error");
                return NotFound();
            }

            return Ok(product);
        }
        #endregion
    }
}