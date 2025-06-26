using System.Collections.Generic;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Classes;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Sensors {
    public class ChestSensor : MultiSensorBase {
        private Chest[] chests;

        public ChestSensor() {
            AddLocalWorldSensor<LogCount>(
                (agent, references) => {
                    var data = references.GetCachedComponent<AgentData>();

                    return data.LogCount;
                });
            
            AddLocalTargetSensor<ClosestChest>(
                (agent, references, target) => {
                    var closestChest = Closest(chests, agent.Transform.position);

                    if (closestChest  == null) {
                        return null;
                    }

                    if (target is TransformTarget transformTarget) {
                        return transformTarget.SetTransform(closestChest.transform);
                    }

                    return new TransformTarget(closestChest.transform);
                });
        }
        
        public override void Created() {}

        public override void Update() {
            chests = Object.FindObjectsOfType<Chest>();
        }

        private T Closest<T>(IEnumerable<T> list, Vector3 position) where T : MonoBehaviour {
            T closest = null;
            var closestDistance = float.MaxValue; // Start with the largest possible distance

            foreach (var item in list) {
                var distance = Vector3.Distance(item.gameObject.transform.position, position);

                if (!(distance < closestDistance))
                    continue;

                closest = item;
                closestDistance = distance;
            }

            return closest;
        }
    }
}