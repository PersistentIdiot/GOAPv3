using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class TreeCutterBrain: AgentBrain {
        protected override void Start() {
            this.provider.RequestGoal<PickupItemGoal<Axe>, IdleGoal>();
        }

        protected override void OnActionEnd(IAction action) {
            if (this.AgentData.hunger > 50 && WorldItems.Any<Pear>()) {
                this.provider.RequestGoal<EatGoal>();
                return;
            }

            if (!this.AgentData.Inventory.Has<Axe>()) {
                this.provider.RequestGoal<PickupItemGoal<Axe>, IdleGoal>();
            }
            else {
                this.provider.RequestGoal<CutTreeGoal, IdleGoal>(); 
            }


            //this.provider.RequestGoal<IdleGoal>();
            // this.provider.AgentTypeBehaviour.AgentType.GoapConfig.GoapInjector.Inject();
        }
    }
}