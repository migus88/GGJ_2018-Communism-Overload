using migs.uMvvm.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace migs.uMvvm.Bindings
{
    [RequireComponent(typeof(Slider))]
    public class SliderBinding : BaseBindable<float, Slider>
    {
        protected override void Start()
        {
            base.Start();
            Holder.onValueChanged.AddListener(this.onValueChanged);
        }

        protected override void handleValueUpdate(float value)
        {
            Holder.value = value;
        }
    }
}