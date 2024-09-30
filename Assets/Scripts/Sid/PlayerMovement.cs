using Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Sid
{
    public class PlayerMovement : MonoBehaviour
    {
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
        private bool _headWillCollide = false;
        private bool _canMove = false;
        private bool _grounded = false;
        private Vector3 _currentVel = Vector3.zero;
        
        private CapsuleCollider _playerCollisionShape;
        private Rigidbody _rigidbody;
        
        void Start()
        {
            _playerCollisionShape = GetComponent<CapsuleCollider>();
            _rigidbody = GetComponent<Rigidbody>();

        }

        // Update is called once per frame
        void Update()
        {
            if (!GlobalVariables.Paused)
            {
                _currentVel = _rigidbody.velocity;
                
                _grounded = _currentVel.y == 0;
                
                if (_grounded && Input.GetKeyDown(KeyCode.Space)) _currentVel.y = jumpVelocity;
                
                UpdateDirectionWASD();
                
                _rigidbody.velocity = _currentVel;
            }
        }

        private void UpdateDirectionWASD()
        {
            // TODO: add stuff here 
            _direction = _direction.normalized;
        }
    }
}
