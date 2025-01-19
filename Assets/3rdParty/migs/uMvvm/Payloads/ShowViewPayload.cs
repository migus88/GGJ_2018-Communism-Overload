using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace migs.uMvvm.Payloads
{
    public class ShowViewPayload
    {
        public string ViewName { get; set; }

        public ShowViewPayload(string viewName)
        {
            ViewName = viewName;
        }
    }
}