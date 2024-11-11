using Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Mapper = Application.AutoMapper.Mapper;

namespace Application.Repositories.ProductRepo
{
    public class Phone : IPhone
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public Phone(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<PhoneProduct>> GetAll()
        {
            var product = await _context.Products.ToListAsync();
            if (product == null) throw new Exception("No data available in (GetAll)");
            var phone = _mapper.Map<IEnumerable<PhoneDto>>(product);

            return product;
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
