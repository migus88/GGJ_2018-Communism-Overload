using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorShredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var env = collision.gameObject.GetComponent<Environment>();
        var t = env != null ? env.Type : PlatformCellPayload.PlatformType.None;

        Destroy(collision.gameObject);
        EventsManager.Instance.Publish(new PlatformCellPayload { EventType = PlatformCellPayload.PlatformEventType.Destroyed, Type = t });
    }
}
