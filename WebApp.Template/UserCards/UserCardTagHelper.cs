using WebApp.Template.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.Template.UserCards
{
    //<user-card app-user=userı bu şekilde attribute olarak verebilmemizi sağlıyor. />
    public class UserCardTagHelper:TagHelper
    {
        public AppUser AppUser { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserCardTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            UserCardTemplate userCardTemplate;
            
            if(_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                userCardTemplate = new PrimeUserCardTemplate();
            }
            else
            {
                userCardTemplate = new DefaultUserCardTemplate();
            }
            userCardTemplate.SetUser(appUser:AppUser);

            //çıktı da ne yazılacak html kodları
            output.Content.SetHtmlContent(userCardTemplate.Build());

        }
    }
}
