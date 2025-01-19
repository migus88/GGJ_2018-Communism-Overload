using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformCellPayload
{
    public PlatformEventType EventType { get; set; }
    public PlatformType Type { get; set; }


    [Serializable]
    public enum PlatformEventType
    {
        Destroyed, Created
    }


    [Serializable]
    public enum PlatformType
    {
        Environment, Invisible, None
    }
}
