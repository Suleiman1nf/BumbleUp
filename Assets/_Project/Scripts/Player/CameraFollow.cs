using System;
using UnityEngine;

namespace Suli.Bumble
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothTime = 0.3f;
        
        private Vector3 _offset;
        private Vector3 _velocity = Vector3.zero;
 
        private void Start()
        {
            _offset = transform.position - _target.position;
        }
 
        private void LateUpdate()
        {
            Vector3 targetPosition = _target.position + _offset;
            targetPosition.x = transform.position.x;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }
    }
}