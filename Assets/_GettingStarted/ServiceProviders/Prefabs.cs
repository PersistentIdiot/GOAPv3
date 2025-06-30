using System.Collections.Generic;
using System.Linq;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using UnityEngine;

namespace _GettingStarted.Services {
    [CreateAssetMenu(fileName = "Prefabs", menuName = "GOAP/Prefabs")]
    public class Prefabs: ScriptableObject {
        [SerializeField] private List<ItemBase> items = new();

        public ItemBase GetItem<TItem>() {
            return items.FirstOrDefault(itemBase => itemBase is TItem);
        }
    }
}