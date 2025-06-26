using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace _GettingStarted.Actions {
    public class PickupLogAction: GoapActionBase<PickupLogAction.Data> {
        public class Data : IActionData {
            public ITarget Target { get; set; }

            // When using the GetComponent attribute, the system will automatically inject the reference
            [GetComponent]
            public AgentData AgentData { get; set; }
        }
        

        public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context) {
            return ActionRunState.WaitThenComplete(0.5f);
        }

        public override void Complete(IMonoAgent agent, Data data) {
            if (data.Target is not TransformTarget transformTarget) {
                return;
            }

            if (transformTarget.Transform.TryGetComponent(out IHoldable holdable)) {
                data.AgentData.Inventory.Add(holdable);
            }

            data.AgentData.LogCount++;
            //GameObject.Destroy(transformTarget.Transform.gameObject);
            transformTarget.Transform.gameObject.SetActive(false);
        }
    }
}