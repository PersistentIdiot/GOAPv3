using CrashKonijn.Agent.Core;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class CrafterBrain : AgentBrain {
        protected override void Start() {
            this.provider.RequestGoal<CraftItemGoal<Axe>, IdleGoal>();
        }

        protected override void OnActionEnd(IAction action) {
            if (this.AgentData.hunger > 50) {
                this.provider.RequestGoal<EatGoal>();
                return;
            }


            this.provider.RequestGoal<CraftItemGoal<Axe>, IdleGoal, HaulItemGoal<Axe>>();
        }
    }
}