using CrashKonijn.Agent.Core;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class HaulerBrain: AgentBrain {
        protected override void Start() {
            this.provider.RequestGoal<HaulLogsGoal, PickupItemGoal<Log>>();
        }
        
        protected override void OnActionEnd(IAction action) {
            if (this.AgentData.hunger > 50) {
                this.provider.RequestGoal<EatGoal>();
                return;
            }


            this.provider.RequestGoal<HaulLogsGoal, PickupItemGoal<Log>>();
        }
    }
}