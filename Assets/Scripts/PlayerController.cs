using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;

namespace Com.Test.TutorialMultiplayer
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        #region Private Fields

        [SerializeField]
        private InputActionReference movementControl;
        [SerializeField]
        private InputActionReference jumpControl;
        [SerializeField]
        private float playerSpeed = 5.0f;
        [SerializeField]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private Transform cameraMainTransform;
        private Transform cameraReference;
        private float rotationSpeed = 4f;
        [SerializeField]
        private GameObject playerCamera;
        private GameObject mainCamera;

        #endregion

        #region Public Fields

        public float Health = 100f;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        public GameObject PlayerUiPrefab;

        #endregion

        public override void OnEnable() {
            base.OnEnable();
            movementControl.action.Enable();
            jumpControl.action.Enable();
        }

        public override void OnDisable() {
            base.OnDisable();
            
            // if (!photonView.IsMine) {
            //     movementControl.action.Disable();
            //     jumpControl.action.Disable();
            //     Debug.Log("NAH INI MASALAHNYA AHAHHAHA");
            // }
        }

        #region IPunObservable Implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if (stream.IsWriting) {
                stream.SendNext(Health);
            } else {
                this.Health = (float)stream.ReceiveNext();
            }
        }

        #endregion

        private void Awake() {
            // #Important
            // Used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine) {
                PlayerController.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // We flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            //DontDestroyOnLoad(this.gameObject);
            // if (!GameObject.Find("Main Camera")) {
            //     DontDestroyOnLoad(Camera.main);
            // } 
        }

        #region MonoBehaviour Callbacks
        
        private void Start()
        {
            controller = gameObject.GetComponent<CharacterController>();
            cameraMainTransform = Camera.main.transform;
            playerCamera = this.gameObject.transform.GetChild(1).gameObject;
            //mainCamera = this.gameObject.transform.GetChild(2).gameObject;

            if (playerCamera != null) {
                if (photonView.IsMine) {
                    playerCamera.SetActive(true);
                    //mainCamera.SetActive(true);
                } else {
                    playerCamera.SetActive(false);
                    //mainCamera.SetActive(false);
                }
            } else {
                Debug.LogError("Missing playerCamera GameObject on Player Prefab");
            }

            if (PlayerUiPrefab != null) {
                GameObject _uiGo = Instantiate(PlayerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            } else {
                Debug.LogWarning("Missing PlayerUiPrefab reference on player Prefab", this);
            }
        }

        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
                return;
            }
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 movement = movementControl.action.ReadValue<Vector2>();
            Vector3 move = new Vector3(movement.x, 0, movement.y);
            move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            // Changes the height position of the player..
            if (jumpControl.action.triggered && groundedPlayer)
            {
                Debug.Log("Jumping");
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            if (movement != Vector2.zero) {
                float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }

            if (photonView.IsMine) {
                if (Health <= 0f) {
                    GameManager.Instance.LeaveRoom();
                }
            }
        }

        #endregion
    }
}