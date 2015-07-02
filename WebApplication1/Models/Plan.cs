namespace WebApplication1.Models
{
    public class Plan
    {
        public int PlanId { get; private set; }
        public string Name { get; private set; }

        public Plan(int planId, string name)
        {
            PlanId = planId;
            Name = name;
        }
    }
}
