using Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Sid
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GameObject headObject;
        [SerializeField] private float jumpVelocity = 4.5f;
        [SerializeField] private float walkingSpeed = 5.0f;
        [SerializeField] private float sprintingSpeed = 8.0f;
        [SerializeField] private float crouchingSpeed = 3.0f;
        [SerializeField] private float mouseSensitivity = 0.4f;
        [SerializeField] private float lerpSpeed = 10.0f;
        [SerializeField] private float crouchCameraY = -0.25f;
        [SerializeField] private float crouchColliderHeight = 1.5f;
        [SerializeField] private float crouchColliderY = -0.25f;
        [SerializeField] private float gravity = -9.8f;
        
        private float _currentSpeed = 5.0f;
        private Vector3 _direction = Vector3.zero;
        private Vector3 _inputDirection = Vector3.zero;
        private bool _headWillCollide = false;
        private bool _canMove = false;
        private bool _grounded = false;
        private Vector3 _currentVel = Vector3.zero;
        
        private CapsuleCollider _playerCollisionShape;
        private Rigidbody _playerRigidbody;
        private Transform _playerTransform;
        
        void Start()
        {
            // getting all the components
            _playerCollisionShape = GetComponent<CapsuleCollider>();
            _playerRigidbody = GetComponent<Rigidbody>();
            _playerTransform = GetComponent<Transform>();
            
            // cursor control
            // Cursor.lockState = CursorLockMode.Confined;
            // Cursor.visible = false;

        }

        // Update is called once per frame
        void Update()
        {
            if (!GlobalVariables.Paused)
            {
                _currentVel = _playerRigidbody.velocity;
                
                _grounded = _currentVel.y == 0;
                
                if (_grounded && Input.GetKeyDown(KeyCode.Space)) _currentVel.y = jumpVelocity;
                
                UpdateInputDirectionWASD();
                _direction = Vector3.Lerp(_direction, _inputDirection.normalized, Time.deltaTime * lerpSpeed);

                if (_direction != Vector3.zero)
                {
                    _currentVel.x = _direction.x * _currentSpeed;
                    _currentVel.z = _direction.z * _currentSpeed;
                }
                else
                {
                    var tempY = _currentVel.y;
                    _currentVel = Vector3.MoveTowards(_currentVel, Vector3.zero, _currentSpeed);
                    _currentVel.y = tempY;
                }
                
                _playerRigidbody.velocity = _currentVel;
            }
        }

        private void UpdateInputDirectionWASD()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _inputDirection.z = 1;
            } 
            else if (Input.GetKey(KeyCode.S))
            {
                _inputDirection.z = -1;
            }
            else
            {
                _inputDirection.z = 0;
            }

            if (Input.GetKey(KeyCode.D))
            {
                _inputDirection.x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _inputDirection.x = -1;
            }
            else
            {
                _inputDirection.x = 0;
            }
        }
    }
}
