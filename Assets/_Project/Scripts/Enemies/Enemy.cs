using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Suli.Bumble
{
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private float _modelOffset;
        [SerializeField] private float _forwardOffset;
        [SerializeField] private float _upOffset;
        [SerializeField] private float _duration;

        public event Action OnDestroy;
        private StairsBuilder _stairsBuilder;
        private IPlayerStairProvider _playerStairProvider;
        private int currStair;

        public void Init(StairsBuilder stairsBuilder, int stair, IPlayerStairProvider playerStairProvider)
        {
            _stairsBuilder = stairsBuilder;
            _playerStairProvider = playerStairProvider;
            currStair = stair;
            var stairPosition = _stairsBuilder.GetStairPosition(currStair) + _modelOffset * Vector3.up;
            transform.localPosition = new Vector3(transform.localPosition.x, stairPosition.y, stairPosition.z);
            StartCoroutine(Move(1));
        }

        public IEnumerator Move(int steps)
        {
            var nextStairPosition = _stairsBuilder.GetStairPosition(currStair - steps);
            Vector3 point1 = transform.localPosition + _upOffset * Vector3.up + _forwardOffset * Vector3.forward;
            Vector3 point2 = new Vector3(transform.localPosition.x, nextStairPosition.y + _modelOffset, nextStairPosition.z);
            Vector3[] path = { point1, point2 };
            transform.DOLocalPath(path, _duration, PathType.CatmullRom);
            yield return new WaitForSeconds(_duration);
            currStair -= steps;
            if (currStair + 3 < _playerStairProvider.PlayerStair)
            {
                OnDestroy?.Invoke();
                Destroy(gameObject);
            }
            else
                StartCoroutine(Move(steps));
        }
    }
}