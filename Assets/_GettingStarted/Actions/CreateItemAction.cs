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
            var animationData = data.AgentData.Animations.Craft;
            data.AgentData.Animazing.Play(animationData.Clip, animationData.Priority);
            return ActionRunState.WaitThenComplete(3f);
        }

        public override void Complete(IMonoAgent agent, Data data) {
            var axePrefab = Services.Services.Get<Prefabs>().GetItem<TItem>();
            
            Vector3 randomOffset = Random.insideUnitSphere * 2;
            randomOffset.y = 0;

            var axe = GameObject.Instantiate(axePrefab);
            axe.transform.position = agent.transform.position + randomOffset;
            axe.Drop(false);
        }

        public class Data : IActionData {
            // When using the GetComponent attribute, the system will automatically inject the reference
            [GetComponent]
            public AgentData AgentData { get; set; }
            public ITarget Target { get; set; }
        }
    }
}