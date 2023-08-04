using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryOnline.Controllers
{
    public class ControllerBase: Controller
    {
        protected bool CheckUserLoginStatus()
        {
            // Kiểm tra xem có tồn tại session đăng nhập không
            bool isLoggedIn = HttpContext.Session.GetString("UserId") != null;
            return isLoggedIn;
        }
    }
}
