using Chebureck.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chebureck.Objects.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Chebureck/SoundData", order = 2)]
    public class SoundData : ScriptableObject
    {
        [SerializeField]
        public List<SoundInfo> sounds;
        public float crossFadeInTime = 2f;
        public float crossFadeOutTime = 1f;


        [Serializable]
        public class SoundInfo
        {
            public SoundEnumerators type;
            public AudioClip clip;
            [Range(0, 1f)]
            public float volume = 1f;
            public bool loop;
            public bool sfx = true;
            public bool crossFade;
        }
    }
}