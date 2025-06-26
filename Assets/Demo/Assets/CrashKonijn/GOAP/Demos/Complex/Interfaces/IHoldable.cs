using UnityEngine;

namespace CrashKonijn.Goap.Demos.Complex.Interfaces
{
    public interface IHoldable
    {
        string DebugName { get; set; }
        GameObject gameObject { get; }
        bool IsHeld { get; }
        bool IsInBox { get; }
        bool IsClaimed { get; }
        GameObject IsClaimedBy { get; set; }

        void Claim(GameObject go);
        void Pickup(GameObject go, bool visible = false);
        void Drop(bool inBox = false);
    }
}