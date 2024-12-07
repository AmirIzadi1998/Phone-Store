using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.DapperRepo;
using Application.Repositories.ProductRepo;
using AutoMapper;
using AutoMapper.Mappers;
using Core.Context;
using Dapper;
using Infrastructure.Dto;
using Infrastructure.Entities;
using Infrastructure.Utility;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Application.Repositories.DapperRepo
{
    public class ProductDapperRepo : IProductDapperRepo
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;
        private readonly IConfiguration _config;
        private readonly QueryUtility _query;
        private readonly NewConnectionUtility _queryUtility;

        public ProductDapperRepo(IMapper mapper, MyContext context, IConfiguration config, QueryUtility query,
            NewConnectionUtility queryUtility)
        {
            _mapper = mapper;
            _context = context;
            _config = config;
            _query = query;
            _queryUtility = queryUtility;
        }

        #region Delete
        public async Task<PhoneProduct> Delete(int id)
        {
            if (id == null) throw new Exception();
            var query = _query.GetQueryDelete();
            using (var connection = _queryUtility.GetConfiguration())
            {
                return await connection.QuerySingleOrDefaultAsync<PhoneProduct>(query, new { Id = id });
            }
        }
        #endregion

        #region GetAll
        public async Task<List<PhoneProduct>> GetAll()
        {
            var query = _query.GetQueryGetAll();
            if (query == null) throw new Exception();
            using (var connection = _queryUtility.GetConfiguration())
            {
                var result = await connection.QueryAsync<PhoneProduct>(query);
                return result.ToList();
            }
        }
        #endregion

        #region GetById
        public async Task<PhoneProduct> GetById(int id)
        {
            if (id == null) throw new Exception();

            var query = _query.GetQueryById();
            using (var connection = _queryUtility.GetConfiguration())
            {
                var result = await connection.QuerySingleOrDefaultAsync<PhoneProduct>(query, new { id = id });
                return result;
            }
        }
        #endregion

        #region GetByName
        public async Task<PhoneProduct> GetByName(string name)
        {
            if (name == null) throw new Exception();
            var query = _query.GetQueryByName();
            using (var connection = _queryUtility.GetConfiguration())
            {
                return await connection.QuerySingleOrDefaultAsync<PhoneProduct>(query, new { PhoneName = name });
            }
        }
        #endregion

        #region Insert
        public async Task<string> Insert(PhoneDto phoneDto)
        {
            if (phoneDto == null) throw new Exception();
            var query = _query.GetQueryInsert();
            using (var connection = _queryUtility.GetConfiguration())
            {
                await connection.ExecuteAsync(query, phoneDto);
                return "success";
            }
        }
        #endregion

        #region Update
        public async Task<PhoneProduct> Update(PhoneDto phoneDto)
        {
            if (phoneDto == null) throw new Exception();
            var query = _query.GetQueryUpdate();
            using (var connection = _queryUtility.GetConfiguration())
            {
               return await connection.QuerySingleOrDefaultAsync<PhoneProduct>(query, phoneDto);
            }
        }
        #endregion
    }
}