using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour {

        public void CloseCredits()
        {
            var parent = transform.GetComponentInParent<CreditsScript>();
            Destroy(parent.gameObject);
        }
    
}
