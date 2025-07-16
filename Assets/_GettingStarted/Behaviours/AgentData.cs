using System;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using CrashKonijn.Goap.Editor;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using Random = System.Random;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class AgentData : MonoBehaviour {
        [Header("References")]
        public ComplexInventoryBehaviour Inventory;
        public Transform LeftHandContainer;
        public Transform RightHandContainer;
        
        [Header("Settings")]
        public float HungerRate = 5;
        
        [Header("Values")]
        public int pearCount = 0;
        public float hunger = 0f;
        public int TreeCount = 0;
        public int LogCount;

        private void Start() {
            HungerRate = UnityEngine.Random.Range(3f, 7f);
        }

        private void Update() {
            // For simplicity, we will increase the hunger over time in this class.
            this.hunger += Time.deltaTime * HungerRate;
        }
    }
}