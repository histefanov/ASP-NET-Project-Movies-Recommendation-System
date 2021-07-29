namespace MoviesRecommendationSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdminRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
