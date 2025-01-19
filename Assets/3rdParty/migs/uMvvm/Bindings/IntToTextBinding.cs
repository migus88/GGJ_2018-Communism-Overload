using migs.uMvvm.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace migs.uMvvm.Bindings
{
    [RequireComponent(typeof(Text))]
    public class IntToTextBinding : BaseBindable<int, Text>
    {
        public string NumberFormat = string.Empty;

        protected override void handleValueUpdate(int obj)
        {
            if (Holder == null)
                return;

            if (string.IsNullOrEmpty(NumberFormat))
                Holder.text = obj.ToString();
            else
                Holder.text = obj.ToString(NumberFormat);
        }
    }
}