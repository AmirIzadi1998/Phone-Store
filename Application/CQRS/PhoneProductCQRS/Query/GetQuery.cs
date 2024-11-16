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
    public class GetQuery : IRequest<GetQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetQueryResponse
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

    public class GetQueryHandler : IRequestHandler<GetQuery, GetQueryResponse>
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public GetQueryHandler(IProductRepository product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        public async Task<GetQueryResponse> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var product = await _product.GetById(request.Id);
            if (product == null) throw new Exception("The desired ID was not found");
            var response = _mapper.Map<GetQueryResponse>(product);
            return response;
        }
    }
}