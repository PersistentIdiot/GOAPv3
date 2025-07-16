using _GettingStarted.Interfaces;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class Axe: ItemBase, IEquipable {
        public bool TryEquip(AgentData data) {
            // If something's in the right hand, fail to equip
            if (data.RightHandContainer.childCount > 0) return false;

            transform.SetParent(data.RightHandContainer);
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            return true;
        }
    }
}