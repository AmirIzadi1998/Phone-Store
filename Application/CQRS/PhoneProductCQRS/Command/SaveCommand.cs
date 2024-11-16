using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.UnitOfWorkRepo;
using AutoMapper;
using Core.Context;
using Infrastructure.Dto;
using Infrastructure.Entities;
using MediatR;

namespace Application.CQRS.PhoneProductCQRS.Command
{
    public class SaveCommand : IRequest<SaveCommandResponse>
    {
        public string PhoneName { get; set; }
        public string PhoneColor { get; set; }
        public long PhoneIMEI { get; set; }
        public bool PhoneIsGlobal { get; set; }
        public string PhoneBattery { get; set; }
        public string PhoneChip { get; set; }
        public bool IsExisting { get; set; }
    }

    public class SaveCommandResponse
    {
        public int PhoneId { get; set; }
    }

    public class SaveCommandHandler : IRequestHandler<SaveCommand, SaveCommandResponse>
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SaveCommandHandler(MyContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<SaveCommandResponse> Handle(SaveCommand request, CancellationToken cancellationToken)
        {
            var result = new PhoneProduct
            {
                PhoneName = request.PhoneName,
                PhoneBattery = request.PhoneBattery,
                PhoneChip = request.PhoneChip,
                PhoneColor = request.PhoneColor,
                PhoneIMEI = request.PhoneIMEI,
                PhoneIsGlobal = request.PhoneIsGlobal,
                IsExisting = request.IsExisting
            };
            //var product = _mapper.Map<PhoneProduct>(request);
            await _context.Products.AddAsync(result);
            _unitOfWork.SaveChangesAsync();
            var response = new SaveCommandResponse()
            {
                PhoneId = result.Id
            };
            return (response);
        }
    }
}
