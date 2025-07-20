using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class BillboardText: MonoBehaviour {
        void Update() {
            transform.rotation = Camera.main!.transform.rotation;
        }
    }
}