using migs.uMvvm.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace migs.uMvvm.Bindings
{
    [RequireComponent(typeof(InputField))]
    public class FloatToInputBinding : BaseBindable<float, InputField>
    {
        public string NumberFormat = string.Empty;

        protected override void Start()
        {
            base.Start();

            Holder.contentType = InputField.ContentType.DecimalNumber;

            Holder.onValueChanged.AddListener((s) =>
            {
                float val = 0;

                float.TryParse(s, out val);
                this.onValueChanged(float.Parse(s));
            });
        }

        protected override void handleValueUpdate(float value)
        {
            if (string.IsNullOrEmpty(NumberFormat))
                Holder.text = value.ToString();
            else
                Holder.text = value.ToString(NumberFormat);
        }
    }
}
