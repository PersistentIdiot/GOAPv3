using CrashKonijn.Agent.Core;
using UnityEngine;

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

            if (!this.AgentData.Inventory.Has<Axe>()) {
                Debug.Log($"Requesting axe pickup");
                this.provider.RequestGoal<PickupItemGoal<Axe>>();
            }


            this.provider.RequestGoal<IdleGoal, CutTreeGoal>();
        }
    }
}