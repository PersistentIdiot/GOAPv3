using System.Collections.Generic;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Sensors {
    public class AxeSensor : MultiSensorBase {
        private Axe[] axes;

        public AxeSensor() {
            AddLocalTargetSensor<ClosestHoldable<Axe>>(
                (agent, references, target) => {
                    var closestAxe = Closest(axes, agent.Transform.position);

                    if (closestAxe == null) {
                        return null;
                    }

                    if (target is TransformTarget transformTarget) {
                        return transformTarget.SetTransform(closestAxe.transform);
                    }

                    return new TransformTarget(closestAxe.transform);
                });
        }

        public override void Created() {}

        public override void Update() {
            axes = Object.FindObjectsOfType<Axe>();
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