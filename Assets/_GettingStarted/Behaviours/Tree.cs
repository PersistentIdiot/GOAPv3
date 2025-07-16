using System;
using System.Collections;
using System.Collections.Generic;
using _GettingStarted.Interfaces;
using CrashKonijn.Docs.GettingStarted.Behaviours;
using CrashKonijn.Goap.Demos.Complex;
using CrashKonijn.Goap.Demos.Complex.Behaviours;
using CrashKonijn.Goap.Demos.Complex.Interfaces;
using UnityEngine;
using Random = System.Random;

public class Tree : ItemBase, IHarvestable {
    [SerializeField] private List<GameObject> Models = new();
    public float TreeRespawnRadius = 10f;
    public float PearDropRadius = 1f;
    public float GrowthProgress
    {
        get => growthProgress;
        set
        {
            growthProgress = value;
            UpdateGrowth(growthProgress);
        }
    }
    public Log LogPrefab;
    public Tree TreePrefab;
    public Pear PearPrefab;
    

    
    private float growthProgress = 1;

    public void Harvest() {
        // Instantiate log
        var log = Instantiate(LogPrefab);
        log.transform.position = transform.position;

        // Respawn tree
        var randomPosition = UnityEngine.Random.insideUnitSphere * TreeRespawnRadius;
        randomPosition.y = 0;

        var newTree = Instantiate(TreePrefab);
        newTree.GrowthProgress = 0.1f;
        newTree.transform.position = randomPosition;

        // Spawn pear
        randomPosition = UnityEngine.Random.insideUnitSphere * PearDropRadius + transform.position;
        randomPosition.y = 0;

        var newPear = Instantiate(PearPrefab);
        newPear.transform.position = randomPosition;
        
        Destroy(gameObject);
    }

    private void Update() {
        if (GrowthProgress >= 1) {
            return;
        }

        GrowthProgress = Mathf.Clamp(GrowthProgress + Time.deltaTime, 0, 1);
        UpdateGrowth(GrowthProgress);
    }

    private void UpdateGrowth(float progress) {
        transform.localScale = progress * Vector3.one;
    }

    private void OnValidate() {
        if (Models.Count == 0) return;
        
        int randomIndex = UnityEngine.Random.Range(0, Models.Count);
        
        for (int i = 0; i < Models.Count; i++) {
            Models[i].SetActive(false);
        }

        Models[randomIndex].SetActive(true);
    }
}