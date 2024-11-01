using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class InfiniteTerrainController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Transform _moveStarter, _moveReplacer;

        private void FixedUpdate()
        {
            transform.position = transform.position -  (_moveSpeed * -1 * Time.deltaTime * transform.right);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MoveReplacer")) {
                transform.position = new Vector3 (_moveStarter.position.x, transform.position.y, transform.position.z);  
            }
        }
    }
}
