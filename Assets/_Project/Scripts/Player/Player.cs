using System;
using DG.Tweening;
using UnityEngine;

namespace Suli.Bumble
{
    public class Player : MonoBehaviour
    {
        [Header("Side settings")]
        [SerializeField] private float _sideDuration;
        [SerializeField] private float _sideOffset;
        [SerializeField] private float _sideUpOffset;

        [Header("Up settings")] 
        [SerializeField] private float _upDuration;
        [SerializeField] private float _upOffset;
        [SerializeField] private float _upBackOffset;
        [SerializeField] private float _modelOffset;

        [Header("Particles settings")] 
        [SerializeField] private ParticleSystem _onStepParticle;
        [SerializeField] private ParticleSystem _onDieParticlePrefab;

        [SerializeField] private PlayerAudio _playerClips;
        
        private Vector3 _currStairPosition;
        private float _startXPosition;
        public event Action OnDie;

        private void Awake()
        {
            _startXPosition = transform.localPosition.x;
        }

        public void SetToStartX()
        {
            transform.localPosition =
                new Vector3(_startXPosition, transform.localPosition.y, transform.localPosition.z);
        }

        public void MoveSide(int direction)
        {
            Vector3 pos = new Vector3(transform.localPosition.x, _currStairPosition.y, _currStairPosition.z);
            Vector3 point1 = pos +
                             (Vector3.right * (direction * _sideOffset) / 2) +
                             (Vector3.up * _sideUpOffset);
            Vector3 point2 = pos +
                             (Vector3.right * (direction * _sideOffset));
            Vector3[] path = { point1, point2 };
            transform.DOLocalPath(path, _sideDuration, PathType.CatmullRom).OnComplete(StepParticle);
            AudioManager.Instance.PlayAudio(_playerClips.MoveClip);
        }

        public void SetToStairPosition(Vector3 stairPosition)
        {
            _currStairPosition = new Vector3(0,stairPosition.y + _modelOffset, stairPosition.z);
            transform.localPosition = new Vector3(transform.localPosition.x, _currStairPosition.y, _currStairPosition.z);
        }
        
        public void MoveUp(Vector3 stairPosition)
        {
            Vector3 currPos = transform.localPosition;
            currPos.y = _currStairPosition.y;
            currPos.z = stairPosition.z;
            _currStairPosition = new Vector3(0,stairPosition.y + _modelOffset, stairPosition.z);
            Vector3 point1 = currPos + Vector3.up * _upOffset - Vector3.back * _upBackOffset;
            Vector3 point2 = new Vector3(transform.localPosition.x, _currStairPosition.y, stairPosition.z);
            Vector3[] path = { point1, point2 };
            transform.DOLocalPath(path, _upDuration, PathType.CatmullRom).OnComplete(StepParticle);
            AudioManager.Instance.PlayAudio(_playerClips.MoveClip);
        }

        private void StepParticle()
        {
            _onStepParticle.Play();
        }

        private void DieParticle()
        {
            GameObject go = Instantiate(_onDieParticlePrefab, null).gameObject;
            go.transform.position = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IDamageDealer>() != null)
            {
                DieParticle();
                AudioManager.Instance.PlayAudio(_playerClips.DieClip);
                OnDie?.Invoke();
            }
        }
    }
}