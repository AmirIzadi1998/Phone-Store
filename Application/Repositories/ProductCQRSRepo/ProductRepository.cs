using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.ProductCQRSRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _context;

        public ProductRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<PhoneProduct> Delete(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PhoneProduct>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<PhoneProduct> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<PhoneProduct> GetByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.PhoneName == name);
        }

        public async Task<int> Insert(PhoneProduct phoneProduct)
        {
            await _context.Products.AddAsync(phoneProduct);
            await _context.SaveChangesAsync();
            return phoneProduct.Id;
        }

        public async Task<PhoneProduct> Update(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}