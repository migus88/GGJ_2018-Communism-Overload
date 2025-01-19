using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusProgressPayload
{
    public float Progress { get; set; }
    public UploadCommandType CommandType { get; set; }


    public enum UploadCommandType
    {
        Progress,
        Pause,
        Reset
    }
}
