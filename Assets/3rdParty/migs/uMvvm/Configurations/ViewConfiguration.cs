using migs.uMvvm.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace migs.uMvvm.Configurations
{
    [Serializable]
    public class ViewConfiguration
    {
        public ViewType Type = ViewType.Page;
        public bool IsDestructible = true;
    }
}