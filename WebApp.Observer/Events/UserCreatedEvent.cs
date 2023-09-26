using BaseProject.Models;
using MediatR;

namespace WebApp.Observer.Events
{
    //Bir bildiri oldugunda calısacak olan kodlar
    public class UserCreatedEvent : INotification
    {
        //Bir bildiri olduğunda içinde appuser olacak.
        public AppUser AppUser { get; set; }
    }
}
