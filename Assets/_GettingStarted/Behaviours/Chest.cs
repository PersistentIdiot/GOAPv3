using System.Collections.Generic;
using _GettingStarted.Interfaces;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class Chest : ItemBase{
        private Dictionary<string, int> items = new();

        public void AddItem(string itemName, int countToAdd = 1) {
            if (items.ContainsKey(itemName)) {
                items[itemName] += countToAdd;
            }
            else {
                items.Add(itemName, countToAdd);
            }
        }
    }
}