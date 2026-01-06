using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    [Authorize(Roles = "Admin")] // double protection
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
            // Any server-side logic here
        }
    }
}
