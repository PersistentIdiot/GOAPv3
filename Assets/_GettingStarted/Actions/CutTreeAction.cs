using _GettingStarted.Interfaces;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace _GettingStarted.Actions {
    public class CutTreeAction: GoapActionBase<CutTreeAction.Data> {
        public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context) {
            // Instead of using a timer, we can use the Wait ActionRunState.
            // The system will wait for the specified time before completing the action
            // Whilst waiting, the Perform method won't be called again
            return ActionRunState.WaitThenComplete(0.5f);
        }

        public override void Complete(IMonoAgent agent, Data data) {
            if (data.Target is not TransformTarget transformTarget)
                return;


            if (transformTarget.Transform.TryGetComponent(out IHarvestable harvestable)) {
                harvestable.Harvest();
            }
            else {
                GameObject.Destroy(transformTarget.Transform.gameObject); 
            }
        }

        public class Data : IActionData {
            public ITarget Target { get; set; }

            // When using the GetComponent attribute, the system will automatically inject the reference
            [GetComponent]
            public AgentData AgentData { get; set; }
        }
    }
}