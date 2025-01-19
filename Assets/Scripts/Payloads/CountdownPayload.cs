using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownPayload
{
    public CountdownCommandType CommandType { get; set; }
    public int Value { get; set; }

    public enum CountdownCommandType
    {
        Show, Hide, Update
    }
}
