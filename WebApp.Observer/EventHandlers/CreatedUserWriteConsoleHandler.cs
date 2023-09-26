using MediatR;
using WebApp.Observer.Events;
using WebApp.Observer.Observer;

namespace WebApp.Observer.EventHandlers
{
    public class CreatedUserWriteConsoleHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<CreatedUserWriteConsoleHandler> _logger;

        public CreatedUserWriteConsoleHandler(ILogger<CreatedUserWriteConsoleHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"user created : Id={notification.AppUser.Id}");

            return Task.CompletedTask;
        }
    }
}
