using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Suli.Bumble
{
    [Serializable]
    public class StairContainer
    {
        public Stair StairPrefab;
        [SerializeField] private int _probabilityCount;

        private float _startProbability;
        private float _endProbability;

        public int ProbabilityCount => _probabilityCount;

        public void SetBounds(float startProbability, float endProbability)
        {
            _startProbability = startProbability;
            _endProbability = endProbability;
        }

        public bool IsContains(float probability)
        {
            return probability > _startProbability && probability < _endProbability;
        }
    }
    
    [Serializable]
    public class StairsBuilder
    {
        [SerializeField] private Stair _safeStairPrefab;
        [SerializeField] private List<StairContainer> _stairContainer;
        [SerializeField] private Transform _container;
        [SerializeField] private float _offsetZ;
        [SerializeField] private float _offsetY;

        [Header("Stairs Color Gradient")] 
        [SerializeField] private Color _firstColor;
        [SerializeField] private Color _secondColor;
        [SerializeField] private int _maxGradientCount;
        private int _currGradientCount;
        private bool _isGradientBack = false;

        private List<Stair> _stairs = new List<Stair>();

        public Vector3 GetStairPosition(int index)
        {
            return _stairs[index].transform.localPosition;
        }

        public void CreateStair(Stair prefab)
        {
            float startY = 0;
            float startZ = 0;
            if (_stairs.Count > 0)
            {
                var previousStair = _stairs[_stairs.Count - 1];
                startY = previousStair.transform.localPosition.y;
                startZ = previousStair.transform.localPosition.z;
            }
            Stair go = GameObject.Instantiate(prefab, _container);
            go.transform.localPosition = new Vector3(0, startY + _offsetY, startZ + _offsetZ);
            go.ChangeColor(GetGradientColor());
            _stairs.Add(go);
        }

        public void CreateRandomStair()
        {
            CreateStair(ChooseRandomStair());
        }

        private Stair InstantiateSafeStairs()
        {
            Stair go = GameObject.Instantiate(_safeStairPrefab, _container);
            return go;
        }

        private Stair ChooseRandomStair()
        {
            float randomValue = Random.Range(0, 1f);
            foreach (var stairContainer in _stairContainer)
            {
                if (stairContainer.IsContains(randomValue))
                    return stairContainer.StairPrefab;
            }

            throw new Exception("Probability set isn't correct");
            return null;
        }

        private void SetProbabilities()
        {
            int summ = 0;
            // calculate summ of probability counts
            foreach (var container in _stairContainer)
            {
                summ += container.ProbabilityCount;
            }
            // caculate probability
            float currX = 0;
            foreach (var container in _stairContainer)
            {
                float probability = (float)container.ProbabilityCount / summ;
                container.SetBounds(currX, currX + probability);
                currX += probability;
            }
        }

        private Color GetGradientColor()
        {
            if (_currGradientCount > _maxGradientCount && !_isGradientBack)
                _isGradientBack = true;
            else if(_currGradientCount <= 0 && _isGradientBack)
            {
                _isGradientBack = false;
            }
            
            if (_isGradientBack)
                _currGradientCount--;
            else
                _currGradientCount++;
            
            return Color.Lerp(_firstColor, _secondColor, (float)_currGradientCount / _maxGradientCount);
        }

        public void DisableLastStair()
        {
            _stairs[0].gameObject.SetActive(false);
        }

        public void ReplaceStairs(int firstStairIndex)
        {
            for (var index = firstStairIndex-1; index < _stairs.Count; index++)
            {
                var stair = _stairs[index];
                var safeStair = InstantiateSafeStairs();
                safeStair.transform.position = stair.transform.position;
                safeStair.ChangeColor(stair.Color);
                _stairs[index] = safeStair;
                GameObject.Destroy(stair.gameObject);
            }
        }
        
        public void CreateStartStairs()
        {
            SetProbabilities();
            _stairs.Clear();
            for (int i = 0; i < 15; i++)
            {
                CreateStair(_safeStairPrefab);
            }
        }
    }
}