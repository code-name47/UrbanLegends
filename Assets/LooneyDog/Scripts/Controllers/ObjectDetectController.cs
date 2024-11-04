using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class ObjectDetectController : MonoBehaviour
    {
        RaycastHit _hit;
        Ray _ray;
        [SerializeField] private float _detectionDistance;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _activeObject;

        public GameObject ActiveObject { get => _activeObject; set => _activeObject = value; }

        private void Update()
        {
            if (Physics.Raycast(transform.position, transform.forward, out _hit, _detectionDistance, _layerMask)){
                if (_hit.transform.CompareTag("Pickable")) {
                    
                    if (ActiveObject == null) {
                        ActiveObject = _hit.transform.gameObject;
                        if (ActiveObject.GetComponent<PickableObjectController>() != null)
                        {
                            ActiveObject.GetComponent<PickableObjectController>().ObjectHighlighter();
                        }
                        else { 
                            //Do Nothing
                        }
                    }
                }
                Debug.DrawRay(transform.position,transform.forward * _hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * 1000, Color.green);
                //Debug.Log("Did not Hit");
                if (ActiveObject != null) {
                    if (ActiveObject.GetComponent<PickableObjectController>() != null)
                    {
                        ActiveObject.GetComponent<PickableObjectController>().DeHightlighter();
                        ActiveObject = null;
                    }
                    else
                    {
                        //Do Nothing
                    }
                }
               
            }
        }
    }
}
