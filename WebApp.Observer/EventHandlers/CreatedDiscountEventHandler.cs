using BaseProject.Models;
using MediatR;
using WebApp.Observer.Events;
using WebApp.Observer.Observer;

namespace WebApp.Observer.EventHandlers
{
    public class CreatedDiscountEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly AppIdentityDbContext _context;
        private readonly ILogger<CreatedDiscountEventHandler> _logger;

        public CreatedDiscountEventHandler(AppIdentityDbContext context, ILogger<CreatedDiscountEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Örneğin sisteme yeni kayıt olan kullanıcıya %10 indirim senaryosu için db'ye ekleme işlemi gerçekleştirildi
            await _context.Discounts.AddAsync(new Models.Discount { Rate = 10, UserId = notification.AppUser.Id });
            await _context.SaveChangesAsync();

            _logger.LogInformation("Discount created");
        }
    }
}
