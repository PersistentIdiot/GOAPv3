using _GettingStarted.Interfaces;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using UnityEngine;

namespace CrashKonijn.Docs.GettingStarted.Behaviours {
    public class StoneMine: ItemBase, IHarvestable {
        public float StoneSpawnRadius = 1;

        [SerializeField] private Stone stonePrefab;
        public void Harvest() {
            Vector3 randomPosition = Random.insideUnitSphere * StoneSpawnRadius + transform.position;
            randomPosition.y = 0;

            var stone = Instantiate(stonePrefab);
            stone.transform.position = randomPosition;
        }
    }
}