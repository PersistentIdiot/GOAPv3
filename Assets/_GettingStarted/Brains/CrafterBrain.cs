using CrashKonijn.Agent.Core;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class CrafterBrain : AgentBrain {
        protected override void Start() {
            this.provider.RequestGoal<CraftItemGoal<Axe>, IdleGoal>();
        }

        protected override void OnActionEnd(IAction action) {
            if (this.AgentData.hunger > 50 && WorldItems.Any<Pear>()) {
                this.provider.RequestGoal<EatGoal>();
                return;
            }

            if (this.AgentData.Inventory.Has<Axe>()) {
                Debug.Log($"Has axe! Requesting haul goal!");
                this.provider.RequestGoal< IdleGoal, HaulItemGoal<Axe>>();
            }
            else {
                this.provider.RequestGoal<CraftItemGoal<Axe>, IdleGoal>(); 
            }


            
        }
    }
}