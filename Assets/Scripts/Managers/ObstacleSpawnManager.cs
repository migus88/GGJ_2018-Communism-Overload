using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleSpawnManager : BaseBehaviour
{
    public Transform DropLocation;
    public GameObject[] ObstaclePrefabs;
    [Range(0, 1f)]
    public float Chance = 0.5f;
    public float SpawnRate = 3f;
    

    // Use this for initialization
    void Start()
    {
        this._typeEvents.Add(typeof(GameStatePayload), EventsManager.Instance.Subscribe<GameStatePayload>(this.onGameStatePayload));
    }

    private void onGameStatePayload(GameStatePayload obj)
    {
        if (GameManager.Instance.State == GameState.Started)
            Invoke("spawnObstacle", SpawnRate);
    }

    private void spawnObstacle()
    {
        if (GameManager.Instance.State != GameState.Started)
            return;
        //TODO: Break loop
        var rnd = UnityEngine.Random.Range(0, 1f);
        if (rnd <= Chance)
        {
            var inst = Instantiate(ObstaclePrefabs[UnityEngine.Random.Range(0, ObstaclePrefabs.Length)], DropLocation.position, DropLocation.rotation);
            inst.transform.position = new Vector3(inst.transform.position.x, inst.transform.position.y, 0);
        }

        Invoke("spawnObstacle", SpawnRate);
    }
}
