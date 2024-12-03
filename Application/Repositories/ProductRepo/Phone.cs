using Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Context;
using Dapper;
using Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mapper = Application.AutoMapper.Mapper;

namespace Application.Repositories.ProductRepo
{
    public class Phone : IPhone
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;
        private readonly IConfiguration _config;

        public Phone(IMapper mapper, MyContext context, IConfiguration config)
        {
            _mapper = mapper;
            _context = context;
            _config = config;
        }

        public async Task<List<PhoneProduct>> GetAll()
        {
            var connectionstring = _config.GetConnectionString("DefaultConnection");
            var query = @"select * from products";
            if (query == null) throw new Exception("No data available in (GetAll)");
            using (var connection = new SqlConnection(connectionstring))
            {
                var result = await connection.QueryAsync<PhoneProduct>(query);
                return result.ToList();
            }
        }

        public async Task<PhoneProduct> GetById(int id)
        {

            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("No data available in (GetById)");
            var phone = _mapper.Map<PhoneDto>(product);

            return product;
        }

        public async Task<PhoneProduct> GetByName(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.PhoneName == name);
            if (product == null) throw new Exception("No data available in (GetByName)");
            var phone = _mapper.Map<PhoneDto>(product);

            return product;
        }

        public async Task<PhoneProduct> Insert(PhoneDto phoneDto)
        {
            var product = _mapper.Map<PhoneProduct>(phoneDto);
            if (product == null) throw new Exception("No data available in (Insert)");
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            phoneDto.Id = product.Id;

            return product;
        }

        public async Task<PhoneProduct> Update(PhoneDto phoneDto)
        {
            
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == phoneDto.Id);
            if (result == null) throw new Exception("This id Not available");
           var mapper = _mapper.Map(phoneDto, result);
            await _context.SaveChangesAsync();

            return mapper;
        }

        public async Task<PhoneProduct> DeleteById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) throw new Exception("This id Not available");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
