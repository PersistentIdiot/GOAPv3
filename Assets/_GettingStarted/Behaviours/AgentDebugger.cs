using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using CrashKonijn.Goap.Runtime;
using TMPro;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class AgentDebugger : MonoBehaviour {
        [SerializeField] private TextMeshPro DebugText;
        private AgentBehaviour agent;
        private AgentData data;

        private void Awake() {
            agent = GetComponent<AgentBehaviour>();
            data = GetComponent<AgentData>();
        }

        private void OnEnable() {
            agent.Events.OnActionStart += action => {
                string debugText = $"{agent.ActionProviderBase.name.Split('(', ')')[1]}\n";
                var arguments = action.GetType().GetGenericArguments();
                
                switch (arguments.Length) {
                    case 0:
                        debugText += $"Action: {action.GetType().Name}";
                        break;
                    case 1:
                        debugText += $"Action<T>: {action.GetType().Name.Replace("`1", "")}<{arguments[0].Name}>";
                        break;
                    default: 
                        debugText += $"Action: {action.GetType().Name}";
                        break;
                }

                /*
                debugText += $"\nInventory: \n";

                var inventoryItems = data.Inventory.GetItems;
                foreach (IHoldable holdable in inventoryItems) {
                    debugText += $"- {holdable.gameObject.name}\n";
                }
                */

                DebugText.text = debugText;
            };
        }

        private void OnDisable() {}
    }
}