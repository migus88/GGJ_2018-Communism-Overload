using migs.uMvvm.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace migs.uMvvm.Bindings
{
    [RequireComponent(typeof(InputField))]
    public class InputBinding : BaseBindable<string, InputField>
    {

        protected override void Start()
        {
            base.Start();
            Holder.onValueChanged.AddListener(this.onValueChanged);
        }

        protected override void handleValueUpdate(string value)
        {
            Holder.text = value;
        }
    }
}
