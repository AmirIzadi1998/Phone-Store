using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.ProductCQRSRepo;
using AutoMapper;
using MediatR;

namespace Application.CQRS.PhoneProductCQRS.Query
{
    public class GetAllQuery:IRequest<List<GetAllQueryRequest>>
    {
    }

    public class GetAllQueryRequest
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

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<GetAllQueryRequest>>
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IProductRepository product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        public async Task<List<GetAllQueryRequest>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var product = await _product.GetAll();
            if (product == null) throw new Exception();
            var response = _mapper.Map<List<GetAllQueryRequest>>(product);
            return response;


        }

    }
}
