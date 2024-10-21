using System.Collections;
using Sound.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Udey.Scripts
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private GameObject image;
        [SerializeField] private float attackCooldown = 2f;
        private float _nextAttackTime = 0f;

        void Update()
        {
            if (Time.time > _nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(FakeAnimation());
                    _nextAttackTime = Time.time + attackCooldown;
                }
            }
        }

        private IEnumerator FakeAnimation()
        {
            image.transform.Rotate(0, 0, 25); // Rotate image to the left by 45 degrees
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.Swing, this.transform.position);
            yield return new WaitForSeconds(0.2f); // Wait for 0.1 seconds
            image.transform.Rotate(0, 0, -25); // Rotate image back to normal
        }
    }
}
