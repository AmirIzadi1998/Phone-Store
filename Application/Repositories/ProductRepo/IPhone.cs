using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dto;
using Infrastructure.Entities;

namespace Application.Repositories.ProductRepo
{
    public interface IPhone
    {
        Task<IEnumerable<PhoneProduct>> GetAll();
        Task<PhoneProduct> GetById(int id);
        Task<PhoneProduct> GetByName(string name);
        Task<PhoneProduct> Insert(PhoneDto phoneDto);
        Task<PhoneProduct> Update(PhoneDto phoneDto);
        Task<PhoneProduct> DeleteById(int id);
    }
}
