using System.Collections.Generic;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Sensors {
    public class StoneSensor: MultiSensorBase {
        private Stone[] stones;

        public StoneSensor() {
            AddLocalTargetSensor<ClosestHoldable<Stone>>(
                (agent, references, target) => {
                    var closestStone = Closest(stones, agent.Transform.position);

                    if (closestStone == null) {
                        return null;
                    }

                    if (target is TransformTarget transformTarget) {
                        return transformTarget.SetTransform(closestStone.transform);
                    }

                    return new TransformTarget(closestStone.transform);
                });
        }
        public override void Created() {
            
        }

        public override void Update() {
            stones = Object.FindObjectsOfType<Stone>();
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