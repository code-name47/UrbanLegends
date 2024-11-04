using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
namespace LooneyDog
{
public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2 _playerDirection;
        [Header("Movement Settings")]
        public float moveSpeed = 5f;            // Speed of movement
        public float rotationSpeed = 15f;       // Speed of camera rotation
        public float lookSensitivity = 2f;      // Sensitivity of mouse movement
        public Transform playerCamera;          // Reference to the camera

        private CharacterController controller; // Reference to the Character Controller
        private Vector2 moveInput;              // Store movement input
        private Vector2 lookInput;              // Store look input
        private float cameraPitch = 0f;         // Pitch value for vertical camera rotation
        private PlayerControls controls;
        [SerializeField] private Animator _ani,_handsAni,_legsAni,_actionCamera;
        [SerializeField] private float _movementTransitionSpeed= 10f;
        private Vector2 _smoothMovement;
        [SerializeField] private float _sprint=2f;
        [SerializeField] private float _cameraVerticalRoatation;
        [SerializeField] private Transform _playerGrabLocation,_grabbedObject;
        [SerializeField] private ObjectDetectController _objectDetectController;

/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
        public Animator Ani { get => _ani; set => _ani = value; }
After:
        public Animator Ani { get => Ani1; set => _ani = value; }
*/
        public Animator Ani { get => Ani1; set => Ani1 = value; }
        public Animator Ani1 { get => _ani; set => Ani1 = value; }

        private void Awake()
        {
            controls = new PlayerControls();
            controller = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            controls.Player.Enable();
            controls.Player.Move.performed += OnMove;
            controls.Player.Move.canceled += OnMove; // Reset moveInput when action is canceled
            controls.Player.Look.performed += OnLook;
            controls.Player.Look.canceled += OnLook;
            controls.Player.Sprint.performed += OnSprint;
            controls.Player.Sprint.canceled += OnSprint;
            controls.Player.Use.performed += OnUse;
            controls.Player.Use.canceled += OnUse;
        }

        private void OnDisable()
        {
            controls.Player.Disable();
            controls.Player.Move.performed -= OnMove;
            controls.Player.Move.canceled -= OnMove;
            controls.Player.Look.performed -= OnLook;
            controls.Player.Look.canceled -= OnLook;
            controls.Player.Sprint.performed -= OnSprint;
            controls.Player.Sprint.canceled -= OnSprint;
            controls.Player.Use.performed -= OnUse;
            controls.Player.Use.canceled -= OnUse;
        }
       

        // This method will be called when the Move input is detected
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed) // When the input is active
            {
                moveInput = context.ReadValue<Vector2>();         
            }
            else if (context.canceled) // When the input is released
            {
                moveInput = Vector2.zero; // Reset to zero when input is not pressed
            }
        }

        // This method will be called when the Look input is detected
        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed) // When the input is active
            {
                lookInput = context.ReadValue<Vector2>();
            }
            else if (context.canceled) // When the input is released
            {
                lookInput = Vector2.zero; // Reset to zero when input is not pressed
            }
            
        }

        public void OnSprint(InputAction.CallbackContext context) {
            if (context.performed) // When the input is active
            {
                _sprint =1f;
            }
            else if (context.canceled) // When the input is released
            {
                
                _sprint = 2f; // Reset to zero when input is not pressed
            }
        }

        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.performed) // When the input is active
            {
                //PerformAction();
                PerformGrab();
            }
            else if (context.canceled) // When the input is released
            {
                //Do nothing
            }
        }

        // Update method to handle movement and looking
        private void Update()
        {
            HandleMovement();
            HandleLook();
        }

        // Handles the movement logic
        private void HandleMovement()
        {
            /*if (moveInput.magnitude > 0)
            {*/
            _smoothMovement.x = Mathf.Lerp(_smoothMovement.x, moveInput.x, _movementTransitionSpeed * Time.deltaTime);
            _smoothMovement.y = Mathf.Lerp(_smoothMovement.y, moveInput.y/_sprint, _movementTransitionSpeed * Time.deltaTime);
            Ani1.SetFloat("xInput", _smoothMovement.x);
            Ani1.SetFloat("yInput", _smoothMovement.y);
            /*_handsAni.SetFloat("xInput", _smoothMovement.x);
            _handsAni.SetFloat("yInput", _smoothMovement.y);
            _legsAni.SetFloat("xInput", _smoothMovement.x);
            _legsAni.SetFloat("yInput", _smoothMovement.y);*/

            /*Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y); // Convert input to movement vector
            move = transform.right * move.x + transform.forward * move.z; // Apply forward/backward, right/left movement
            controller.Move(move * moveSpeed * Time.deltaTime); // Move character based on movement vector*/
            /*}*/
        }

        // Handles the look-around logic (camera rotation)
        private void HandleLook()
        {
            // Horizontal rotation (turning left/right)
            transform.Rotate(Vector3.up * lookInput.x * lookSensitivity);
            
            // Vertical rotation (looking up/down)
            cameraPitch -= lookInput.y * lookSensitivity;           // Invert Y-axis for natural look
            cameraPitch = Mathf.Clamp(cameraPitch, -1*_cameraVerticalRoatation, _cameraVerticalRoatation);      // Clamp the vertical rotation
            Ani1.SetFloat("LookInput", -1*cameraPitch/ _cameraVerticalRoatation);
            playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }
        public void PerformAction() {
            _actionCamera.gameObject.SetActive(true);
            Ani1.SetTrigger("Vault");
            _actionCamera.SetTrigger("Vault");
            Ani1.SetLayerWeight(1, 0);
            Ani1.SetLayerWeight(2, 0);
        }

        public void PerformGrab() {
            
            Ani1.SetTrigger("Grab");
            if (_grabbedObject != null)
            {
                Ani1.SetBool("Grabbed", false);
                //DropObject();
            }
            else
            {
                if (_objectDetectController.ActiveObject != null)
                {
                    Ani1.SetBool("Grabbed", true);
                    _grabbedObject = _objectDetectController.ActiveObject.transform;

                    /* if (_grabbedObject == null)
                     {
                         PickObject();
                     }*/
                }
            }
            /*else
            {
                if (_objectDetectController.ActiveObject != null)
                {
                    Ani1.SetBool("Grabbed", true);
                    if (_grabbedObject == null)
                    {
                        PickObject();
                    }
                }
            }*/


        }

        public void GrabObject() {
            /*if (_grabbedObject == null )
            {*/
                PickObject();
            //}
        }


        public void PickObject() {
            /*if (_objectDetectController.ActiveObject != null)
            {
                _objectDetectController.ActiveObject.transform.parent = _playerGrabLocation;
                _objectDetectController.ActiveObject.GetComponent<Rigidbody>().isKinematic = true;
                _objectDetectController.ActiveObject.transform.localPosition = Vector3.zero;
                _objectDetectController.ActiveObject.transform.localRotation = Quaternion.identity;
                _grabbedObject = _objectDetectController.ActiveObject.transform;
            }*/

            if (_grabbedObject != null)
            {
                _grabbedObject.transform.parent = _playerGrabLocation;
                _grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                _grabbedObject.transform.localPosition = Vector3.zero;
                _grabbedObject.transform.localRotation = Quaternion.identity;
                
            }
        }

        public void DropObject() {
            if (_grabbedObject != null)
            {
                _grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                _grabbedObject.transform.parent = null;
                _grabbedObject = null;
            }
        }
    }
}
