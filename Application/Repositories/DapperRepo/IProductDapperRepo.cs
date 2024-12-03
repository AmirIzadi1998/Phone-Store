using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dto;
using Infrastructure.Entities;

namespace Application.Repositories.DapperRepo
{
    public interface IProductDapperRepo
    {
        Task<List<PhoneProduct>> GetAll();
        Task<PhoneProduct> GetById(int id);
        Task<PhoneProduct> GetByName(string name);
        Task<string> Insert(PhoneDto phoneDto);
        Task<PhoneProduct> Update(PhoneDto phoneDto);
        Task<PhoneProduct> Delete(int id);
    }
}
