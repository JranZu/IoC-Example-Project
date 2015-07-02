using System;

namespace WebApplication1.Models
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            this.planRepository = planRepository;
        }

        public int Create(Plan plan)
        {
            planRepository.Insert(plan);
            return plan.PlanId;
        }
    }
}
