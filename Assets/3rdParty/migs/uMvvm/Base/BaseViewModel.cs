using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace migs.uMvvm.Base
{
    [System.Serializable]
    public abstract class BaseViewModel
    {
        public string ViewModelName { get; protected set; }

        public abstract void Init();

        public BaseViewModel()
        {
            ViewModelName = this.GetType().Name;
        }
    }
}
