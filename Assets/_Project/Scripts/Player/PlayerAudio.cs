using System;
using UnityEngine;

namespace Suli.Bumble
{
    [Serializable]
    public class PlayerAudio
    {
        [SerializeField] private AudioClip _moveClip;
        [SerializeField] private AudioClip _dieClip;

        public AudioClip DieClip => _dieClip;

        public AudioClip MoveClip => _moveClip;
    }
}