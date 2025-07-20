using System.Collections.Generic;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Sensors {
    public class AnvilSensor : MultiSensorBase {
        private Anvil[] anvils;

        public AnvilSensor() {
            AddLocalTargetSensor<ClosestAnvil>(
                (agent, references, target) => {
                    var closestAnvil = Closest(anvils, agent.Transform.position);

                    if (closestAnvil == null) {
                        return null;
                    }

                    if (target is TransformTarget transformTarget) {
                        return transformTarget.SetTransform(closestAnvil.transform);
                    }

                    return new TransformTarget(closestAnvil.transform);
                });
        }

        public override void Created() {}

        public override void Update() {
            anvils = Object.FindObjectsOfType<Anvil>();
        }

        // Returns the closest item in a list
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