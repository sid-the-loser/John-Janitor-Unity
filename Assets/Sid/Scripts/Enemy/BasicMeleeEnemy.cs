using System;
using Sid.Scripts.Common;
using Sid.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sid.Scripts.Enemy
{
    public class BasicMeleeEnemy : MonoBehaviour
    {
        private static float enemySpeed = 3.0f;
        
        private GameObject _player;
        private Rigidbody _rigidbody;
        
        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _rigidbody = GetComponent<Rigidbody>();

            if (_player is null)
            {
                Debug.LogError("Player not detected!");
            }
        }

        
        void Update()
        {
            if (!GlobalVariables.Paused)
            {
                _rigidbody.isKinematic = false;
                
                print(enemySpeed);
                
                if (_player is not null)
                {
                    var currentVel = Vector3.zero;
                    var direction = (_player.transform.position - transform.position).normalized;

                    currentVel = direction * enemySpeed;

                    currentVel.y = _rigidbody.velocity.y;

                    _rigidbody.velocity = currentVel;
                }
            }
            else
            {
                _rigidbody.isKinematic = true;
            }

        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public static void UpdateStats()
        {
            enemySpeed = GlobalVariables.MoveSpeed;
        }
    }
}
