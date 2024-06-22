using UnityEngine;

namespace Game.Scripts.Characters
{
    public class PlayerController : MonoBehaviour
    {
        private Player _playerInput;
        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        public float playerSpeed = 2.0f;
        [SerializeField] private float playerExtraTurnSpeed = 1.0f;
        // [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        public PlayerData playerData;
        public bool randomSideMovement;
    
        // Awake is called once before the first execution of Start after the MonoBehaviour is created
        void Awake()
        {
            _playerInput = new Player();
            _controller = GetComponent<CharacterController>();
        }
    
        void OnEnable()
        {
            _playerInput.Enable();
        }
    
        void OnDisable()
        {
            _playerInput.Disable();
        }

        void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }
        
            Vector2 movementInput = _playerInput.PlayerMain.Move.ReadValue<Vector2>();
            if (movementInput != Vector2.zero)
            {
                movementInput.y = 1;
            }
            else
            {
                // make player rotation to 0
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Camera.main == null)
            {
                Debug.LogError("Main Camera not found at PlayerController");
                return;
            }
        
            // make character incline towards the direction of movement 
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
        
            Vector3 move = forward * movementInput.y + right * (movementInput.x * playerExtraTurnSpeed);
        
            if (PauseInputController.IsPaused)
            {
                //freeze position
                move = Vector3.zero;
            }
            
            _controller.Move(move * (Time.deltaTime * playerSpeed));

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player...
            // we don't jump yet
            // if (_playerInput.PlayerMain.Jump.triggered && _groundedPlayer)
            // {
            //     _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            // }
        
            _playerVelocity.y += gravityValue * Time.deltaTime;
            
            if (randomSideMovement)
            {
                if (Random.Range(0, 100) < 5)
                {
                    // lock between x 3 and -3
                    var currentX = gameObject.transform.position.x;
                    var startX = currentX < -2 ? 1 : -5;
                    var endX = currentX > 2 ? -1 : 5;
                    _playerVelocity.x += Random.Range(startX, endX);
                }
            }
            if (PauseInputController.IsPaused)
            {
                // freeze velocity
                _playerVelocity = Vector3.zero;
            }
            
            _controller.Move(_playerVelocity * Time.deltaTime);
            
        }
    }
}
