using System;
using _GettingStarted.Interfaces;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace _GettingStarted.Actions {
    public class PickupItemAction<TItem> : GoapActionBase<PickupItemAction<TItem>.Data> {
        public enum PickupState {
            Kneeling,
            PickingUp,
            Standing
        }

        public override bool IsValid(IActionReceiver agent, Data data) {
            if (data.Target is not TransformTarget transformTarget) return false;
            if (!transformTarget.Transform.TryGetComponent(out IHoldable holdable) || holdable.IsClaimed) return false;
            return base.IsValid(agent, data);
        }

        public override void Start(IMonoAgent agent, Data data) {
            base.Start(agent, data);
            // Init animations, putting it in data for no good reason
            data.AnimationStart = data.AgentData.Animations.PickupFromGroundStart;
            data.AnimationLoop = data.AgentData.Animations.PickupFromGroundLoop;
            data.AnimationEnd = data.AgentData.Animations.PickupFromGroundEnd;

            data.State = PickupState.Kneeling;
        }

        public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context) {
            data.Timer -= Time.deltaTime;

            if (data.Timer > 0) {
                return ActionRunState.Continue;
            }

            switch (data.State) {
                case PickupState.Kneeling:
                    data.Timer = data.AnimationStart.Clip.length;
                    data.AgentData.Animazing.Play(data.AnimationStart.Clip, data.AnimationStart.Priority);
                    data.State = PickupState.PickingUp;
                    return ActionRunState.Continue;
                    break;
                case PickupState.PickingUp:
                    data.Timer = data.AnimationLoop.Clip.length;
                    data.AgentData.Animazing.Play(data.AnimationLoop.Clip, data.AnimationLoop.Priority);
                    data.State = PickupState.Standing;
                    return ActionRunState.Continue;
                    break;
                case PickupState.Standing:
                    data.AgentData.Animazing.Play(data.AnimationEnd.Clip, data.AnimationEnd.Priority);
                    return ActionRunState.Completed;
                default:
                    return ActionRunState.Stop;
            }

            /*
            if (data.Target is TransformTarget transformTarget && transformTarget.Transform.TryGetComponent(out IHoldable holdable)) {
                Debug.Log($"{agent.transform.gameObject.name} picked up {holdable.gameObject.name}");
            }
            data.AgentData.Animazing.Play(data.AgentData.Animations.PickupFromGround.Clip, data.AgentData.Animations.PickupFromGround.Priority);
            return ActionRunState.WaitThenComplete(data.AgentData.Animations.PickupFromGround.Clip.length);
            */
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
            public PickupState State = PickupState.Kneeling;
            public float Timer = 0;

            public AnimationData AnimationStart;
            public AnimationData AnimationLoop;
            public AnimationData AnimationEnd;
        }
    }
}