using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using TMPro;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class AgentDebugger : MonoBehaviour {
        [SerializeField] private TextMeshPro DebugText;
        private AgentBehaviour agent;

        private void Awake() {
            this.agent = this.GetComponent<AgentBehaviour>();
        }

        private void OnEnable() {
            this.agent.Events.OnActionStart += action => {
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

                DebugText.text = debugText;
            };
        }

        private void OnDisable() {}
    }
}