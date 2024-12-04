using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.ProductCQRSRepo;
using AutoMapper;
using Core.Context;
using MediatR;

namespace Application.CQRS.PhoneProductCQRS.Command
{
    public class DeleteCommand : IRequest<DeleteCommandResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteCommandResponse
    {
        public string success { get; set; }
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, DeleteCommandResponse>
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public DeleteCommandHandler(IProductRepository product, IMapper mapper, MyContext context)
        {
            _product = product;
            _mapper = mapper;
            _context = context;
        }

        public async Task<DeleteCommandResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var response = await _product.Delete(request.Id);
            if (request.Id == null) throw new Exception();
            
                var remove = _context.Remove(response);
                await _context.SaveChangesAsync();
                var id = new DeleteCommandResponse()
                {
                    success = "success"
                };

                return id;
            
        }
    }
}