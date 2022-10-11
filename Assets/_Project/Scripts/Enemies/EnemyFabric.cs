using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Suli.Bumble
{
    [Serializable]
    public class EnemyFabric
    {
        [SerializeField] private Transform _containter;
        [SerializeField] private List<Enemy> _enemiePrefabs;
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        
        private StairsBuilder _stairsBuilder;
        private IPlayerStairProvider _playerStairProvider;

        public void Init(StairsBuilder stairsBuilder, IPlayerStairProvider playerStairProvider)
        {
            _stairsBuilder = stairsBuilder;
            _playerStairProvider = playerStairProvider;
        }
        
        private Enemy ChooseRandomEnemy()
        {
            return _enemiePrefabs[Random.Range(0, _enemiePrefabs.Count)];
        }

        public void DeleteAllEnemies()
        {
            foreach (Transform child in _containter)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        
        public void CreateEnemy()
        {
            Enemy enemy = GameObject.Instantiate(ChooseRandomEnemy(), _containter);
            enemy.transform.localPosition = new Vector3(Random.Range(_minX, _maxX), enemy.transform.localPosition.y, enemy.transform.localPosition.z);
            enemy.Init(_stairsBuilder, _playerStairProvider.PlayerStair + 10, _playerStairProvider);
            enemy.OnDestroy += CreateEnemy;
        }    
    }
}