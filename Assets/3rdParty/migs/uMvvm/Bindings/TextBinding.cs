using migs.uMvvm.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace migs.uMvvm.Bindings
{
    [RequireComponent(typeof(Text))]
    public class TextBinding : BaseBindable<string, Text>
    {
        protected override void handleValueUpdate(string obj)
        {
            if (Holder != null)
                Holder.text = obj;
        }
    }
}