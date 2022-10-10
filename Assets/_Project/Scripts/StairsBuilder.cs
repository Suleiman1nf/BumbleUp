using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suli.Bumble
{
    [Serializable]
    public class StairsBuilder
    {
        [SerializeField] private Transform container;
        [SerializeField] private float offsetZ;
        [SerializeField] private float offsetY;
        
        private List<Stair> _stairs = new List<Stair>();

        public Vector3 GetStairPosition(int index)
        {
            return _stairs[index].transform.localPosition;
        }

        private void DeleteLastStair()
        {
            GameObject.Destroy(_stairs[0].gameObject);
            _stairs.RemoveAt(0);
        }
        
        public void AddStair(Stair stair)
        {
            float startY = 0;
            float startZ = 0;
            if (_stairs.Count > 0)
            {
                var previousStair = _stairs[_stairs.Count - 1];
                startY = previousStair.transform.localPosition.y;
                startZ = previousStair.transform.localPosition.z;
            }
            Stair go = GameObject.Instantiate(stair, container);
            go.transform.localPosition = new Vector3(0, startY + offsetY, startZ + offsetZ);
            _stairs.Add(go);
        }
    }
}