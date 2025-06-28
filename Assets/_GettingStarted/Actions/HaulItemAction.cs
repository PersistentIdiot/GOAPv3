using System.Linq;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Goap;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace _GettingStarted.Actions {
    public class HaulItemAction<TItem> : GoapActionBase<HaulItemAction<TItem>.Data> where TItem : ItemBase {
        public override void Start(IMonoAgent agent, Data data) {
            var item = data.AgentData.Inventory.Get<TItem>().FirstOrDefault();

            if (item is null) {
                Debug.Log($"Item is null!");
                return;
            }

            item.Claim(agent.gameObject);
            
            
            data.Item = item;
        }

        public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context) {
            return ActionRunState.WaitThenComplete(0.5f);
        }

        public override void Complete(IMonoAgent agent, Data data) {
            data.Inventory.Remove(data.Item);
            data.AgentData.LogCount--;
        }

        public class Data : IActionData {
            public ITarget Target { get; set; }
            public IHoldable Item { get; set; }
            // When using the GetComponent attribute, the system will automatically inject the reference
            [GetComponent]
            public AgentData AgentData { get; set; }
            
            [GetComponent]
            public ComplexInventoryBehaviour Inventory { get; set; }
        }
    }
}