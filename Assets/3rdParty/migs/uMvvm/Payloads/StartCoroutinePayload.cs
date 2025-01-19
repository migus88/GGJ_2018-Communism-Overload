using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace migs.uMvvm.Payloads
{
    public class StartCoroutinePayload
    {
        public IEnumerator Routine { get; set; }

        public StartCoroutinePayload(IEnumerator routine)
        {
            Routine = routine;
        }
    }
}