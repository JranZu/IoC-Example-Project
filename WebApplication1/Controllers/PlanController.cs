using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int planId, string name)
        {
            int newPlanId = planService.Create(new Plan(planId, name));

            ViewData["PlanId"] = newPlanId;
            ViewData["PlanName"] = name;

            return View();
        }
    }
}