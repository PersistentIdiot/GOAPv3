using CrashKonijn.Agent.Core;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class HaulerBrain : AgentBrain {
        protected override void Start() {
            this.provider.RequestGoal<HaulItemGoal<Stone>, PickupItemGoal<Stone>, HaulItemGoal<Log>, PickupItemGoal<Log>>();
        }

        protected override void OnActionEnd(IAction action) {
            if (this.AgentData.hunger > 50 && WorldItems.Any<Pear>()) {
                this.provider.RequestGoal<EatGoal>();
                return;
            }


            this.provider.RequestGoal<HaulItemGoal<Stone>, PickupItemGoal<Stone>, HaulItemGoal<Log>, PickupItemGoal<Log>>();
        }
    }
}