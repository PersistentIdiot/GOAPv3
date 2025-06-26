using System.Collections.Generic;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Sensors {
    public class LogSensor: MultiSensorBase {
        public Log[] logs;

        public LogSensor() {
            AddLocalWorldSensor<LogCount>(
                (agent, references) => {
                    var data = references.GetCachedComponent<AgentData>();

                    return data.LogCount;
                } );
            
            AddLocalTargetSensor<ClosestLog>(
                (agent, references, target) => {
                    var closestLog = Closest(logs, agent.Transform.position);

                    if (closestLog == null) {
                        return null;
                    }

                    if (target is TransformTarget transformTarget) {
                        return transformTarget.SetTransform(closestLog.transform);
                    }

                    return new TransformTarget(closestLog.transform);
                });
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

        public override void Created() {
        }

        public override void Update() {
            logs = Object.FindObjectsOfType<Log>();
        }
    }
}