using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class PickableObjectController : MonoBehaviour
    {
        [SerializeField] GameObject _originalObject, _outLinedObject;
        public void ObjectHighlighter() {
            _originalObject.SetActive(false);
            _outLinedObject.SetActive(true);
        }

        public void DeHightlighter() {
            _originalObject.SetActive(true);
            _outLinedObject.SetActive(false);
        }
    }
}
