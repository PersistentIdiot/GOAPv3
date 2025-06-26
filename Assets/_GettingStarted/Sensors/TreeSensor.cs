using System.Collections;
using System.Collections.Generic;
using CrashKonijn.Docs.GettingStarted;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

public class TreeSensor : MultiSensorBase {
    private Tree[] trees;
    public TreeSensor() {
        this.AddLocalWorldSensor<TreeCount>(
            (agent, references) => {
                var data = references.GetCachedComponent<AgentData>();

                return data.TreeCount;
            });
        
        this.AddLocalTargetSensor<ClosestTree>(
            (agent, references, target) => {
                var closestTree = Closest(trees, agent.Transform.position);

                if (closestTree == null) {
                    return null;
                }

                if (target is TransformTarget transformTarget) {
                    return transformTarget.SetTransform(closestTree.transform);
                }

                return new TransformTarget(closestTree.transform);
            });
    }
    public override void Created() {
    }

    public override void Update() {
        trees = Object.FindObjectsOfType<Tree>();
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
