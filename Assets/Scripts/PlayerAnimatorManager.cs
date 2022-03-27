using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Test.TutorialMultiplayer
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        #region Private Fields

        private Animator animator;
        private CharacterController controller;
        private Rigidbody rb;
        private PlayerInput input;
        private PlayerInputActions inputActions;

        [SerializeField]
        private float directionDampTime = 0.25f;

        #endregion

        #region MonoBehaviour Callbacks

        void Awake() {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            rb = GetComponent<Rigidbody>();

            input = GetComponent<PlayerInput>();

            inputActions = new PlayerInputActions();
            inputActions.Player.Enable();
            inputActions.UI.Disable();
        }

        void Start() {
            if (!animator) {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
            if (!controller) {
                Debug.LogError("PlayerAnimatorManager is Missing CharacterController Component", this);
            }
        }

        void FixedUpdate() {
            // deal with Jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // only allow jumping if we are running
            if (stateInfo.IsName("Base Layer.Run")) {
                //When using trigger parameter
                inputActions.Player.Jump.performed += Jump;
            }
            if (!animator) {
                return;
            }
            Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
            float h = inputVector.x;
            float v = inputVector.y;
            if (v < 0) {
                v = 0;
            }
            animator.SetFloat("Speed", v);
            rb.AddForce(transform.TransformPoint(animator.GetFloat("Speed") * 10f, 0, 0));

            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
            transform.Rotate(new Vector3(0, 1, 0) * h * 100f * Time.deltaTime);
        }

        public void Jump(InputAction.CallbackContext context) {
            if (context.performed) {
                rb.AddForce(Vector3.up * 0.1f, ForceMode.Impulse);
            }
        }
        
        #endregion
    }
}

