using migs.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    protected readonly Dictionary<Type, Guid> _typeEvents = new Dictionary<Type, Guid>();
    protected readonly Dictionary<string, Guid> _nameEvents = new Dictionary<string, Guid>();

    protected virtual void OnDestroy()
    {
        foreach (var item in this._typeEvents)
        {
            EventsManager.Instance.Unsubscribe(item.Key, item.Value);
        }
        foreach (var item in this._nameEvents)
        {
            EventsManager.Instance.Unsubscribe(item.Key, item.Value);
        }
    }
}
