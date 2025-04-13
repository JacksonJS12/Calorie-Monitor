using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CaloryMonitor.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public BaseController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public string GetUserId()
        {
            return _userManager.GetUserId(User);
        }
    }
}
