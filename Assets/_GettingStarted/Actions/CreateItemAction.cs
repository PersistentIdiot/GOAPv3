using System;
using System.Linq;
using _GettingStarted.Services;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Goap;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GettingStarted.Actions {
    public class CreateItemAction<TItem> : GoapActionBase<CreateItemAction<TItem>.Data, CreateItemAction<TItem>.Props> where TItem : ItemBase {
        private InstanceHandler instanceHandler;

        public override void BeforePerform(IMonoAgent agent, Data data) {
            if (instanceHandler == null) {
                instanceHandler = GameObject.FindObjectOfType<InstanceHandler>();
            }
            
            base.BeforePerform(agent, data);
        }

        public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context) {
            var animationData = data.AgentData.Animations.Craft;
            data.AgentData.Animazing.Play(animationData.Clip, animationData.Priority);

            return ActionRunState.WaitThenComplete(Properties.craftingTime);
        }

        public override void Complete(IMonoAgent agent, Data data) {
            var axePrefab = Services.Services.Get<Prefabs>().GetItem<TItem>();
            RemoveReagents(data);
            Vector3 randomOffset = Random.insideUnitSphere * 2;
            randomOffset.y = 0;

            var axe = GameObject.Instantiate(axePrefab);
            axe.transform.position = agent.transform.position + randomOffset;
            axe.Drop(false);
        }

        private void RemoveReagents(Data data) {
            // Wood
            for (int i = 0; i < Properties.requiredWood; i++) {
                var wood = data.AgentData.Inventory.Get<Log>().FirstOrDefault();
                data.AgentData.Inventory.Remove(wood);
                instanceHandler.QueueForDestroy(wood);
            }
            
            // Stone
            for (int i = 0; i < Properties.requiredStone; i++) {
                var stone = data.AgentData.Inventory.Get<Stone>().FirstOrDefault();
                data.AgentData.Inventory.Remove(stone);
                instanceHandler.QueueForDestroy(stone);
            }
        }

        [Serializable]
        public class Props : IActionProperties {
            // These are set in the capability builder's SetProperties method
            public float craftingTime;
            public int requiredWood;
            public int requiredStone;
        }

        public class Data : IActionData {
            // When using the GetComponent attribute, the system will automatically inject the reference
            [GetComponent]
            public AgentData AgentData { get; set; }
            public ITarget Target { get; set; }
        }
    }
}