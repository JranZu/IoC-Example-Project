using WebApplication1.Controllers;
using WebApplication1.Models;

using InversionControl;

namespace WebApplication1.IoC
{
    public static class BootStrapper
    {
        public static void Configure(IContainer container)
        {
            container.Register<PlanController, PlanController>();
            container.Register<IPlanService, PlanService>();
            container.Register<IPlanRepository, PlanRepository>();
        }
    }
}