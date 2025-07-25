﻿using System.Collections.Generic;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using UnityEngine;

namespace CrashKonijn.Goap.Demos.Complex.Behaviours
{
    public abstract class ItemBase : MonoBehaviour, IHoldable {
        public List<ItemBase> Reagents = new();
        private ItemCollection collection;
        
        [field: SerializeField]
        public bool IsHeld { get; private set; }
        
        [field: SerializeField]
        public bool IsInBox { get; private set; }
        
        public bool IsClaimed => this.IsClaimedBy != null;
            
        [field: SerializeField]
        public GameObject IsClaimedBy { get; set; }

        public string DebugName { get; set; }

        public void Awake()
        {
            this.collection = Compatibility.FindObjectOfType<ItemCollection>();
        }

        public void OnEnable()
        {
            this.collection.Add(this);
        }

        public void OnDisable()
        {
            this.collection.Remove(this);
        }

        public void Claim(GameObject go)
        {
            this.IsClaimedBy = go;
        }

        public void Pickup(GameObject go, bool visible = false)
        {
            this.IsHeld = true;
            this.IsInBox = false;
            this.IsClaimedBy = go;

            if (this == null || this.gameObject == null)
                return;
            
            foreach (var renderer in this.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.enabled = visible;
            }
        }

        public void Drop(bool inBox = false)
        {
            this.IsHeld = false;
            this.IsInBox = inBox;
            this.IsClaimedBy = null;
            
            foreach (var renderer in this.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.enabled = true;
            }
        }
    }
}