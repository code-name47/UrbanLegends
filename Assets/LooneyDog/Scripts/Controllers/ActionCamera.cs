using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class ActionCamera : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        public void DisableCameraAfterAction() {
            gameObject.SetActive(false);
            _player.Ani.SetLayerWeight(1, 1);
            _player.Ani.SetLayerWeight(2, 1);
        }
    }
}
