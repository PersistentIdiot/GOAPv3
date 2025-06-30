using _GettingStarted.Services;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace _GettingStarted.Actions {
    public class CreateItemAction<TItem> : GoapActionBase<CreateItemAction<TItem>.Data> where TItem : ItemBase {
        public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context) {
            return ActionRunState.WaitThenComplete(2.5f);
        }

        public override void Complete(IMonoAgent agent, Data data) {
            var axePrefab = Services.Services.Get<Prefabs>().GetItem<TItem>();
            
            Vector3 randomOffset = Random.insideUnitSphere * 2;
            randomOffset.y = 0;

            var axe = GameObject.Instantiate(axePrefab);
            axe.transform.position = agent.transform.position + randomOffset;
            axe.Drop(false);
            
            // Remove reagents
            foreach (ItemBase reagent in axe.Reagents) {
                if (!data.AgentData.Inventory.TryRemoveItem(reagent)) {
                    Debug.Log($"Agent doesn't have any {reagent.gameObject.name}");
                }
            }
        }

        public class Data : IActionData {
            public TItem Item;
            // When using the GetComponent attribute, the system will automatically inject the reference
            [GetComponent]
            public AgentData AgentData { get; set; }
            public ITarget Target { get; set; }
        }
    }
}