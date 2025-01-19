using migs.uMvvm.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace migs.uMvvm.Bindings
{
    [RequireComponent(typeof(Text))]
    public class FloatToTextBinding : BaseBindable<float, Text>
    {
        public string NumberFormat = string.Empty;

        protected override void handleValueUpdate(float obj)
        {
            if (string.IsNullOrEmpty(NumberFormat))
                Holder.text = obj.ToString();
            else
                Holder.text = obj.ToString(NumberFormat);
        }
    }
}