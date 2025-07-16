using _GettingStarted.Interfaces;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace _GettingStarted.Actions {
    public class PickupItemAction<TItem> : GoapActionBase<PickupItemAction<TItem>.Data> {
        public override bool IsValid(IActionReceiver agent, Data data) {
            if (data.Target is not TransformTarget transformTarget) return false;
            if (!transformTarget.Transform.TryGetComponent(out IHoldable holdable) || holdable.IsClaimed) return false;
            return base.IsValid(agent, data);
        }

        public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context) {
            // Instead of using a timer, we can use the Wait ActionRunState.
            // The system will wait for the specified time before completing the action
            // Whilst waiting, the Perform method won't be called again
            if (data.Target is TransformTarget transformTarget && transformTarget.Transform.TryGetComponent(out IHoldable holdable)) {
                Debug.Log($"{agent.transform.gameObject.name} picked up {holdable.gameObject.name}");
            }
            else {
                Debug.Log($"Message");
            }
            return ActionRunState.WaitThenComplete(0.5f);
        }

        // This method is called when the action is completed
        public override void Complete(IMonoAgent agent, Data data) {
            if (data.Target is not TransformTarget transformTarget)
                return;

            
            
            if (transformTarget.Transform.TryGetComponent(out IHoldable item)) {
                item.Pickup(data.AgentData.gameObject);
                data.AgentData.Inventory.Add(item);

                if (transformTarget.Transform.TryGetComponent(out IEquipable equipable)) {
                    if (!equipable.TryEquip(data.AgentData)) {
                        Debug.Log($"{data.AgentData.gameObject} failed to equip {item.gameObject.name}");
                    }
                    else {
                        Debug.Log($"{data.AgentData.gameObject} equipped {item.gameObject.name}");
                    }
                }
                else {
                    item.gameObject.SetActive(false);
                }
            }
            else {
                GameObject.Destroy(transformTarget.Transform.gameObject);
            }
        }

        // The action class itself must be stateless!
        // All data should be stored in the data class
        public class Data : IActionData {
            public ITarget Target { get; set; }

            // When using the GetComponent attribute, the system will automatically inject the reference
            [GetComponent]
            public AgentData AgentData { get; set; }
        }

        
    }
}