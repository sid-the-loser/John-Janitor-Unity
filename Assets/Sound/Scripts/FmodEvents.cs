using System;
using UnityEngine;
using FMODUnity;

namespace Sound.Scripts
{
    public class FmodEvents : MonoBehaviour
    {
        [field: Header("Character Noises")]
        [field: SerializeField] public EventReference Walk { get; private set; }
        public static FmodEvents Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            if (Instance == null)
            {
                Debug.LogError("More then 1 instance");
            }
        }
    }
}
