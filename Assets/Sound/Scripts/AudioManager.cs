using FMODUnity;
using FMOD.Studio;
using UnityEditor.Timeline;
using UnityEngine;

namespace Sound.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        private GameObject _player;
        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            Instance = this;
            if (Instance == null)
            {
                Debug.LogError("More then one audio manager");
            }
        }

        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }

        public EventInstance CreateEventInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(_player.transform.position));
            return eventInstance;
        }
    }
}
