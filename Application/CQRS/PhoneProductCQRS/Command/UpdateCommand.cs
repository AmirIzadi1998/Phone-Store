using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.ProductCQRSRepo;
using AutoMapper;
using Core.Context;
using Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.PhoneProductCQRS.Command
{
    public class UpdateCommand:IRequest<UpdateCommandResponse>
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

    public class UpdateCommandResponse
    {
        public string Success { get; set; }
    }
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, UpdateCommandResponse>
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public UpdateCommandHandler(IProductRepository product, IMapper mapper, MyContext context)
        {
            _product = product;
            _mapper = mapper;
            _context = context;
        }
        public async Task<UpdateCommandResponse> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {

            var response = await _product.Update(request.Id);

            if (response == null) throw new Exception();
            var mapper = _mapper.Map(request, response);
            await _context.SaveChangesAsync();

            return new UpdateCommandResponse() { Success = "success" };

        }
    }
}
