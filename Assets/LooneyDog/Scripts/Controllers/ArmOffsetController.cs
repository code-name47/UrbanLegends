using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class ArmOffsetController : MonoBehaviour
    {
        public Transform leftHandTransform;
        public Transform rightHandTransform;
        public Transform cameraTransform;  // Reference to the camera transform
        public Vector3 armOffset;          // The desired offset for the hands
        public Quaternion armOffsetRotation;
        private void LateUpdate() // Use LateUpdate to ensure it runs after animations are applied
        {
            // Adjust the position of the left and right hand based on the camera position
            /*leftHandTransform.position = cameraTransform.position + cameraTransform.right * armOffset.x + cameraTransform.up * armOffset.y + cameraTransform.forward * armOffset.z;*/
            rightHandTransform.position = cameraTransform.position + cameraTransform.right * -armOffset.x + cameraTransform.up * armOffset.y + cameraTransform.forward * armOffset.z;

            // Optionally, adjust rotation to follow the camera's forward direction
            /*leftHandTransform.rotation = cameraTransform.rotation;
            rightHandTransform.rotation = cameraTransform.rotation;*/
            /*leftHandTransform.rotation = new Quaternion(leftHandTransform.rotation.x + armOffsetRotation.x, leftHandTransform.rotation.y + armOffsetRotation.y
                , leftHandTransform.rotation.z +armOffsetRotation.z, leftHandTransform.rotation.w);
            rightHandTransform.rotation = new Quaternion(rightHandTransform.rotation.x - armOffsetRotation.x, rightHandTransform.rotation.y - armOffsetRotation.y
                , rightHandTransform.rotation.z -armOffsetRotation.z, rightHandTransform.rotation.w);*/
        }
    }
}
