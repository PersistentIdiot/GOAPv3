using System;
using _GettingStarted.Interfaces;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours
{
    public class Pear : MonoBehaviour, IHoldable {
        public float MinLifetime = 1;
        public float MaxLifetime = 30;
        float lifeTime = Single.MaxValue;

        private void Start() {
            lifeTime = UnityEngine.Random.Range(MinLifetime, MaxLifetime);
        }

        private void Update() {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0) {
                Destroy(gameObject);
            }
        }

        public string DebugName { get; set; }
        public bool IsHeld { get; set; }
        public bool IsInBox { get; set; }
        public bool IsClaimed { get; set; }
        public GameObject IsClaimedBy { get; set; }
        public void Claim(GameObject go) {
            throw new NotImplementedException();
        }

        public void Pickup(GameObject go, bool visible = false) {
            throw new NotImplementedException();
        }

        public void Drop(bool inBox = false) {
            throw new NotImplementedException();
        }
    }
}