using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsDialog : MonoBehaviour
{

    public void Close()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Close();
        }
    }
}
