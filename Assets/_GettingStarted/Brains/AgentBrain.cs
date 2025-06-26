using System;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class AgentBrain : MonoBehaviour {
        public string AgentType = "DemoAgent";
        protected AgentBehaviour agent;
        protected GoapActionProvider provider;
        protected GoapBehaviour goap;
        protected AgentData AgentData;

        private void Awake() {
            this.goap = FindObjectOfType<GoapBehaviour>();
            this.agent = this.GetComponent<AgentBehaviour>();
            this.provider = this.GetComponent<GoapActionProvider>();
            this.AgentData = this.GetComponent<AgentData>();

            // This only applies to the code demo
            if (this.provider.AgentTypeBehaviour == null) {
                this.provider.AgentType = this.goap.GetAgentType(AgentType);
            }
        }

        protected virtual void Start() {
            //this.provider.RequestGoal<IdleGoal, CutTreeGoal, HaulLogsGoal>();
        }

        private void OnEnable() {
            this.agent.Events.OnActionEnd += this.OnActionEnd;
        }

        private void OnDisable() {
            this.agent.Events.OnActionEnd -= this.OnActionEnd;
        }

        protected virtual void OnActionEnd(IAction action) {

            if (this.AgentData.hunger > 50) {
                this.provider.RequestGoal<EatGoal>();
                return;
            }



            this.provider.RequestGoal<IdleGoal>();
        }
    }
}