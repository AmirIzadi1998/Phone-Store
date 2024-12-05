using Application.Repositories.DapperRepo;
using Application.Repositories.DapperRepo;
using Core.Context;
using Infrastructure.Dto;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phone_Store.Controllers
{
    [Route("api/ProductWithDapper")]
    [ApiController]
    [Authorize]
    public class ProductWithDapperController : ControllerBase
    {
        private readonly IProductDapperRepo _dapperRepo;
        private readonly MyContext _context;

        public ProductWithDapperController(IProductDapperRepo dapperRepo, MyContext context)
        {
            _dapperRepo = dapperRepo;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dapperRepo.GetAll();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _dapperRepo.GetById(id);
            return Ok(response);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _dapperRepo.GetByName(name);
            return Ok(response);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(PhoneDto phoneDto)
        {
            var response = await _dapperRepo.Insert(phoneDto);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(PhoneDto phoneDto)
        {
            var response = await _dapperRepo.Update(phoneDto);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _dapperRepo.Delete(id);
            return Ok(response);
        }
    }
}
