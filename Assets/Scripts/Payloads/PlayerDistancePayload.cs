using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistancePayload
{
    public PlayersState State { get; set; }

    [Serializable]
    public enum PlayersState
    {
        InRange,
        FarAway
    }
}
