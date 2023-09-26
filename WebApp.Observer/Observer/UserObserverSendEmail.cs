using BaseProject.Models;
using System.Net;
using System.Net.Mail;

namespace WebApp.Observer.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser appUser)
        {
            //ToDo: Burada kesinlikle mail bilgilerinizi girmeniz bekleniyor.
     
        }
    }
}
