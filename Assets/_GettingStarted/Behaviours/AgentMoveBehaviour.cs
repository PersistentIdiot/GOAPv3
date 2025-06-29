using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class AgentMoveBehaviour : MonoBehaviour {
        public float MoveSpeed = 1;
        private AgentBehaviour agent;
        private ITarget currentTarget;
        private bool shouldMove;

        private void Awake() {
            this.agent = this.GetComponent<AgentBehaviour>();
        }

        private void OnEnable() {
            this.agent.Events.OnTargetInRange += this.OnTargetInRange;
            this.agent.Events.OnTargetChanged += this.OnTargetChanged;
            this.agent.Events.OnTargetNotInRange += this.TargetNotInRange;
            this.agent.Events.OnTargetLost += this.TargetLost;
        }

        private void OnDisable() {
            this.agent.Events.OnTargetInRange -= this.OnTargetInRange;
            this.agent.Events.OnTargetChanged -= this.OnTargetChanged;
            this.agent.Events.OnTargetNotInRange -= this.TargetNotInRange;
            this.agent.Events.OnTargetLost -= this.TargetLost;
        }

        private void TargetLost() {
            this.currentTarget = null;
            this.shouldMove = false;
        }

        private void OnTargetInRange(ITarget target) {
            this.shouldMove = false;
        }

        private void OnTargetChanged(ITarget target, bool inRange) {
            this.currentTarget = target;
            this.shouldMove = !inRange;
        }

        private void TargetNotInRange(ITarget target) {
            this.shouldMove = true;
        }

        public void Update() {
            if (this.agent.IsPaused)
                return;

            if (!this.shouldMove)
                return;

            if (this.currentTarget == null)
                return;

            this.transform.position = Vector3.MoveTowards(
                this.transform.position,
                new Vector3(this.currentTarget.Position.x, this.transform.position.y, this.currentTarget.Position.z),
                Time.deltaTime * MoveSpeed);

            Vector3 facingDirection = currentTarget.Position - transform.position;
            facingDirection.y = 0;
            
            transform.forward = facingDirection;
        }

        private void OnDrawGizmos() {
            if (this.currentTarget == null)
                return;

            Gizmos.DrawLine(this.transform.position, this.currentTarget.Position);
        }
    }
}