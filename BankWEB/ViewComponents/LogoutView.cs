using Microsoft.AspNetCore.Mvc;

namespace BankWEB.ViewComponents
{
    public class LogoutView : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
