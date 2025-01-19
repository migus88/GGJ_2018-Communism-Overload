using migs.uMvvm.Configurations;
using migs.uMvvm.Enums;
using migs.uMvvm.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace migs.uMvvm.Base
{
    public abstract class BaseView : MonoBehaviour
    {
        public ViewConfiguration Configuration;

        public virtual BaseViewModel ViewModel { get; set; }


        public virtual void EnterAnimation(Action callback = null)
        {
            //Do nothing. If overriden can play enterance animation
            callback?.Invoke();
        }

        public virtual void ExitAnimation(Action callback = null)
        {
            //Do nothing. If overriden can play exit animation
            callback?.Invoke();
        }
    }
    
    public abstract class BaseView<TViewModel> : BaseView where TViewModel : BaseViewModel, new()
    {
        public new TViewModel ViewModel { get; set; }

        private bool _isNeedToInit = true;

        protected virtual void Awake()
        {
            this.init();
        }

        protected virtual void OnDisable()
        {
            this._isNeedToInit = true;
        }

        protected virtual void OnEnable()
        {
            if (this._isNeedToInit)
                this.init();
        }

        protected virtual void init()
        {
            ViewModel = new TViewModel();
            ViewModel.Init();
            this._isNeedToInit = false;
        }
    }
}