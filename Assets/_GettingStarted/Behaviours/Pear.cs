using System;
using _GettingStarted.Interfaces;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours
{
    public class Pear : ItemBase {
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
    }
}