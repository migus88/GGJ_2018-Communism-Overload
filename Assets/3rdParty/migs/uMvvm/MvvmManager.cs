using migs.EventSystem;
using migs.uMvvm.Base;
using migs.uMvvm.Payloads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace migs.uMvvm
{
    [RequireComponent(typeof(Canvas))]
    public class MvvmManager : MonoBehaviour
    {
        protected Dictionary<string, BaseView> _views = new Dictionary<string, BaseView>();
        protected Dictionary<Type, Guid> _subscriptionKeys = new Dictionary<Type, Guid>();
        protected Canvas _canvas;

        #region Static methods

        public static void ShowView(string viewName)
        {
            EventsManager.Instance.Publish(new ShowViewPayload(viewName));
        }

        public static void RunCoroutine(IEnumerator routine)
        {
            EventsManager.Instance.Publish(new StartCoroutinePayload(routine));
        }

        #endregion

        //TODO: Handle dialogs

        protected virtual void showView(ShowViewPayload payload)
        {
            var displayedView = this._views.FirstOrDefault(v => v.Value.gameObject.activeSelf && v.Value.Configuration.Type == Enums.ViewType.Page);

            if(displayedView.Value != null)
            {
                displayedView.Value.ExitAnimation(() =>
                {
                    this.hideView(displayedView);
                    this.instantiateView(payload.ViewName);
                });
            }
            else
            {
                this.instantiateView(payload.ViewName);
            }
        }

        private void startCoroutine(StartCoroutinePayload obj)
        {
            StartCoroutine(obj.Routine);
        }

        private void instantiateView(string viewName)
        {
            if (this._views.ContainsKey(viewName))
            {
                this._views[viewName].gameObject.SetActive(true);
            }
            else
            {
                var prefab = Resources.Load<BaseView>($"{viewName}/{viewName}");

                if (prefab == null) //TODO: print to console
                    return;

                var obj = Instantiate(prefab, transform);
                this._views.Add(viewName, obj);
            }

            this._views[viewName]?.EnterAnimation();
        }

        private void hideView(KeyValuePair<string, BaseView> displayedView)
        {
            if (displayedView.Value.Configuration.IsDestructible)
            {
                Destroy(displayedView.Value.gameObject);
                this._views.Remove(displayedView.Key);
            }
            else
            {
                displayedView.Value.gameObject.SetActive(false);
            }
        }

        #region Lifecycle methods

        protected virtual void Awake()
        {
            this._canvas = GetComponent<Canvas>();

            this._subscriptionKeys.Add(typeof(ShowViewPayload), EventsManager.Instance.Subscribe<ShowViewPayload>(this.showView));
            this._subscriptionKeys.Add(typeof(StartCoroutinePayload), EventsManager.Instance.Subscribe<StartCoroutinePayload>(this.startCoroutine));
        }

        protected virtual void OnDestroy()
        {
            foreach (var item in this._subscriptionKeys)
            {
                EventsManager.Instance.Unsubscribe(item.Key, item.Value);
            }
        }

        #endregion
    }
}