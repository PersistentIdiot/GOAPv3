using CrashKonijn.Agent.Core;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class MinerBrain: AgentBrain {
        protected override void Start() {
            this.provider.RequestGoal<HarvestGoal<StoneMine>>();
        }
        
        protected override void OnActionEnd(IAction action) {
            if (this.AgentData.hunger > 50) {
                this.provider.RequestGoal<EatGoal>();
                return;
            }


            this.provider.RequestGoal<HarvestGoal<StoneMine>>();
        }
    }
}