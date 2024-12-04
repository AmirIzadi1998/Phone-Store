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
        Task<List<PhoneProduct>> GetAll();
        Task<PhoneProduct> GetById(int id);
        Task<PhoneProduct> GetByName(string name);
        Task<PhoneProduct> Delete(int id);
        Task<PhoneProduct> Update(int id);
        Task<int> Insert(PhoneProduct phoneProduct);
    }
}
