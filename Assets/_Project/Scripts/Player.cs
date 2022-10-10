using System;
using DG.Tweening;
using UnityEngine;

namespace Suli.Bumble
{
    public class Player : MonoBehaviour
    {
        [Header("Side settings")]
        [SerializeField] private float sideDuration;
        [SerializeField] private float sideOffset;
        [SerializeField] private float sideUpOffset;

        [Header("Up settings")] 
        [SerializeField] private float upDuration;
        [SerializeField] private float upOffset;
        [SerializeField] private float upBackOffset;
        [SerializeField] private float modelOffset;


        private Vector3 currStairPosition;

        public void MoveSide(int direction)
        {
            Vector3 pos = new Vector3(transform.localPosition.x, currStairPosition.y, currStairPosition.z);
            Vector3 point1 = pos +
                             (Vector3.right * (direction * sideOffset) / 2) +
                             (Vector3.up * sideUpOffset);
            Vector3 point2 = pos +
                             (Vector3.right * (direction * sideOffset));
            Vector3[] path = { point1, point2 };
            transform.DOLocalPath(path, sideDuration, PathType.CatmullRom);
        }

        public void SetToStairPosition(Vector3 stairPosition)
        {
            currStairPosition = new Vector3(0,stairPosition.y + modelOffset, stairPosition.z);
            transform.localPosition = new Vector3(transform.localPosition.x, currStairPosition.y, currStairPosition.z);
        }
        
        public void MoveUp(Vector3 stairPosition)
        {
            Vector3 currPos = transform.localPosition;
            currPos.y = currStairPosition.y;
            currPos.z = stairPosition.z;
            currStairPosition = new Vector3(0,stairPosition.y + modelOffset, stairPosition.z);
            Vector3 point1 = currPos + Vector3.up * upOffset - Vector3.back * upBackOffset;
            Vector3 point2 = new Vector3(transform.localPosition.x, currStairPosition.y, stairPosition.z);
            Vector3[] path = { point1, point2 };
            transform.DOLocalPath(path, upDuration, PathType.CatmullRom);
        }
    }
}