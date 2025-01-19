using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlatformChecker scrpt = GetComponent<PlatformChecker>();
#if UNITY_WEBGL || UNITY_EDITOR_WIN
        Destroy(scrpt.gameObject);
#endif
    }

}
