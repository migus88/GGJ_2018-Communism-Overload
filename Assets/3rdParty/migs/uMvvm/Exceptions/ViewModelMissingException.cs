using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace migs.uMvvm.Exceptions
{
    public class ViewModelMissingException : Exception
    {
        public ViewModelMissingException(string viewModelName) : base($"'{viewModelName}' is missing a ViewModel") { }
    }
}