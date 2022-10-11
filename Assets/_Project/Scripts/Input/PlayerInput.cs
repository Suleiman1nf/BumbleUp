using System;
using UnityEngine;

namespace Suli.Bumble
{
    public class PlayerInput
    {
        public Action OnMoveLeft;
        public Action OnMoveRight;
        public Action OnMoveUp;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnMoveRight?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnMoveLeft?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                OnMoveUp?.Invoke();
            }
        }
    }
}