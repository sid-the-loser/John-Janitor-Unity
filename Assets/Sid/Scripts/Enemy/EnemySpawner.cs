using System;
using UnityEngine;

namespace Sid.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab; // temporary

        private bool _enemiesSpawned = false;
        
        void Start()
        {
            
        }

        
        void Update()
        {
        
        }


        private void SpawnEnemies()
        {
            _enemiesSpawned = true;
            Debug.Log("Functionality not added yet!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_enemiesSpawned)
            {
                SpawnEnemies();
            }
        }
    }
}
