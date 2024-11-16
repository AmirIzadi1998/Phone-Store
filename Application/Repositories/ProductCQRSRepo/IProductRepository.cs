using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Application.Repositories.ProductCQRSRepo
{
    public interface IProductRepository
    {
        Task<IEnumerable<PhoneProduct>> GetAll();
        Task<PhoneProduct> GetById(int id);
        Task<int> Insert(PhoneProduct phoneProduct);
    }
}
