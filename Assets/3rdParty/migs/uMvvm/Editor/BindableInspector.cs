using migs.uMvvm.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace migs.uMvvm.CustomInspector
{
    [CustomEditor(typeof(BaseBindable), true)]
    public class BindableInspector : Editor
    {
        public int ModelIndex = 0;
        public int PropertyIndex = 0;

        public string[] ModelNames => this._models.Select(m => m.Name).ToArray();
        public string[] PropertyNames => this._properties.Select(m => m.Name).ToArray();

        private Type _viewModel;
        private List<PropertyInfo> _models;
        private List<PropertyInfo> _properties;

        private BaseBindable _target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            this._target = (BaseBindable)target;

            GUILayout.Label(string.Empty);
            GUILayout.Label("Bind To:");

            this.getViewModel();
            this.getModels();
            this.getProperties();

            GUILayout.Label($"{this._target.ViewModelName}.{this._target.ModelPropertyName}.{this._target.PropertyName}");
            GUILayout.Label(string.Empty);
        }
        private void getProperties()
        {
            if (this._models == null || this._models.Count == 0)
                return;

            var baseProperties = typeof(BaseModel).GetProperties().ToList();
            this._properties = this._models[ModelIndex].PropertyType.GetProperties().Where(p => !baseProperties.Exists(bp => bp.Name == p.Name)).ToList();

            var names = PropertyNames.ToList();

            if (names.Exists(n => n == this._target.PropertyName))
                PropertyIndex = names.FindIndex(n => n == this._target.PropertyName);

            PropertyIndex = EditorGUILayout.Popup(PropertyIndex >= PropertyNames.Length ? 0 : PropertyIndex, PropertyNames);

            this._target.PropertyName = this.PropertyNames[PropertyIndex];
        }

        private void getModels()
        {
            if (this._viewModel == null)
                return;

            this._models = new List<PropertyInfo>();
            var props = this._viewModel.GetProperties();
            foreach (var prop in props)
            {
                if (typeof(BaseModel).IsAssignableFrom(prop.PropertyType))
                    this._models.Add(prop);
            }

            if (this._models.Count == 0)
                return;

            var modelsList = ModelNames.ToList();
            if (modelsList.Exists(n => n == this._target.ModelPropertyName))
                ModelIndex = modelsList.FindIndex(n => n == this._target.ModelPropertyName);

            ModelIndex = EditorGUILayout.Popup(ModelIndex >= ModelNames.Length ? 0 : ModelIndex, ModelNames);
            this._target.ModelPropertyName = ModelNames[ModelIndex];
        }

        private void getViewModel()
        {
            if (this._viewModel != null)
                return;

            var view = this._target.GetComponentInParent<BaseView>();
            if (view != null)
            {
                this._viewModel = Type.GetType($"{view.GetType().Name}Model, Assembly-CSharp");
            }

            this._target.ViewModelName = this._viewModel.Name;
        }
    }
}