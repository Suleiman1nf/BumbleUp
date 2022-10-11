using System;
using DG.Tweening;
using UnityEngine;

namespace Suli.Bumble
{
    public class HandMover : MonoBehaviour
    {
        [SerializeField] private Transform hand;
        [SerializeField] private Transform endPos;

        public void Start()
        {
            hand.DOMove(endPos.position, 1f).SetLoops(-1, LoopType.Restart);
        }
    }
}