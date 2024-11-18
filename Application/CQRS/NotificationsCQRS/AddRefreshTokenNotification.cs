using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Context;
using Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.NotificationsCQRS
{
    public class AddRefreshTokenNotification : INotification
    {
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
        public int RefreshTokenTimeOut { get; set; }
    }

    public class AddRefreshTokenNotificationHandler : INotificationHandler<AddRefreshTokenNotification>
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public AddRefreshTokenNotificationHandler(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Handle(AddRefreshTokenNotification notification, CancellationToken cancellationToken)
        {
            var userrefreshtoken = _mapper.Map<UserRefreshToken>(notification);

            var check = await _context.UserRefreshTokens.SingleOrDefaultAsync(x => x.UserId == notification.UserId);
            if (check == null)
            {
                await _context.AddAsync(userrefreshtoken);
            }
            else
            {
                check.RefreshToken = userrefreshtoken.RefreshToken;
                check.RefreshTokenTimeOut = userrefreshtoken.RefreshTokenTimeOut;
                check.CreateDate = DateTime.Now;
                check.Isvalid = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
