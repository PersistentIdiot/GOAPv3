using System;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    [CreateAssetMenu(fileName = "Default Agent Animations", menuName = "GOAP/Agent Animations")]
    public class AgentAnimations: ScriptableObject {
        public AnimationData Idle;
        public AnimationData Walk;
        public AnimationData CutTree;
        public AnimationData Mine;
        public AnimationData PickupFromGroundStart;
        public AnimationData PickupFromGroundLoop;
        public AnimationData PickupFromGroundEnd;
    }

    [Serializable]
    public struct AnimationData {
        public AnimationClip Clip;
        public int Priority;
    }
}