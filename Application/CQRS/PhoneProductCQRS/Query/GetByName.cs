using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.ProductCQRSRepo;
using Application.Repositories.ProductRepo;
using AutoMapper;
using Infrastructure.Dto;
using MediatR;

namespace Application.CQRS.PhoneProductCQRS.Query
{
    public class GetByName : IRequest<GetByNameResponse>
    {
        public string name { get; set; }
    }

    public class GetByNameResponse
    {
        public int Id { get; set; }
        public string PhoneName { get; set; }
        public string PhoneColor { get; set; }
        public long PhoneIMEI { get; set; }
        public bool PhoneIsGlobal { get; set; }
        public string PhoneBattery { get; set; }
        public string PhoneChip { get; set; }
        public bool IsExisting { get; set; }
    }

    public class GetByNameHandler : IRequestHandler<GetByName, GetByNameResponse>
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public GetByNameHandler(IProductRepository product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        public async Task<GetByNameResponse> Handle(GetByName request, CancellationToken cancellationToken)
        {
            var product = await _product.GetByName(request.name);
            if (product == null) throw new Exception();
            var response = _mapper.Map<GetByNameResponse>(product);
            return response;
        }
    }
}