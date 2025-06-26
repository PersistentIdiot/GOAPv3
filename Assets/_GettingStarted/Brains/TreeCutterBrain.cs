using CrashKonijn.Agent.Core;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class TreeCutterBrain: AgentBrain {
        protected override void Start() {
            this.provider.RequestGoal<IdleGoal, CutTreeGoal>();
        }

        protected override void OnActionEnd(IAction action) {
            if (this.AgentData.hunger > 50) {
                this.provider.RequestGoal<EatGoal>();
                return;
            }


            this.provider.RequestGoal<IdleGoal, CutTreeGoal>();
        }
    }
}