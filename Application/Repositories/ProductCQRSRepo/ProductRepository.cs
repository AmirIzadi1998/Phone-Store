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
        public async Task<IEnumerable<PhoneProduct>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<PhoneProduct> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<int> Insert(PhoneProduct phoneProduct)
        {
            await _context.Products.AddAsync(phoneProduct);
            await _context.SaveChangesAsync();
            return phoneProduct.Id;
        }
    }
}
