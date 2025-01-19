using migs.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : BaseBehaviour
{
    public PlatformCellPayload.PlatformType PlatformType = PlatformCellPayload.PlatformType.Invisible;
    public GameObject FloorPrefab;
    public Transform SpawnPoint;
    public float CellWidth = 1f;

    // Use this for initialization
    void Start()
    {
        this._typeEvents.Add(typeof(PlatformCellPayload), EventsManager.Instance.Subscribe<PlatformCellPayload>(this.onPlatformEvent));
    }

    private void onPlatformEvent(PlatformCellPayload obj)
    {
        if (obj.EventType == PlatformCellPayload.PlatformEventType.Destroyed && obj.Type == PlatformType)
        {
            this.createNewTile();
        }
    }

    private void createNewTile()
    {
        var obj = Instantiate(FloorPrefab, transform) as GameObject;
        obj.transform.position = SpawnPoint.transform.position;
        SpawnPoint.SetAsLastSibling();
        SpawnPoint.transform.position = new Vector3(SpawnPoint.transform.position.x + CellWidth, SpawnPoint.transform.position.y);
    }
}