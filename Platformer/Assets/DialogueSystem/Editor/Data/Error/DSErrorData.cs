using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DSErrorData<Type> where Type : ISetStyleError
    {
        GlobalCounterErrors _counter;
        readonly Color _errorColor = Color.red;
        readonly List<Type> _elements = new();

        public bool isEmpty => _elements.Count == 0;

        public void Add(Type element)
        {
            if (_elements.Count == 1)
            {
                _counter.AddErrors();
                _elements.First().SetErrorStyle(_errorColor);
            }
            _elements.Add(element);
            if (_elements.Count > 1)
                element.SetErrorStyle(_errorColor);
        }

        public void Remove(Type element)
        {
            element.SetDefaultStyle();
            _elements.Remove(element);
            if (_elements.Count == 1)
            {
                _counter.SubstractErrors();
                _elements.First().SetDefaultStyle();
            }

        }

        public DSErrorData(GlobalCounterErrors counter)
        {
            _counter = counter;
        }
    }

}