using System;
using UnityEngine;

namespace Suli.Bumble
{
    public interface IPlayerInput
    {
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnMoveUp;

        public void Update();
    }

    public class KeyboardPlayerInput : IPlayerInput
    {
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnMoveUp;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
                OnMoveUp?.Invoke();
            else if(Input.GetKeyDown(KeyCode.D))
                OnMoveRight?.Invoke();
            else if(Input.GetKeyDown(KeyCode.A))
                OnMoveLeft?.Invoke();
        }
    }
    
    public class GesturePlayerInput : IPlayerInput
    {
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnMoveUp;
        
        // If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
        private const float MAX_SWIPE_TIME = 0.5f; 
	
        // Factor of the screen width that we consider a swipe
        // 0.17 works well for portrait mode 16:9 phone
        private const float MIN_SWIPE_DISTANCE = 0.17f;

        Vector2 startPos;
        float startTime;

        public void Update()
        {

            if(Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if(t.phase == TouchPhase.Began)
                {
                    startPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);
                    startTime = Time.time;
                }
                if(t.phase == TouchPhase.Ended)
                {
                    if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
                        return;

                    Vector2 endPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);

                    Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

                    if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
                    {
                        OnMoveUp?.Invoke();
                        return;
                    } 

                    if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y)) { // Horizontal swipe
                        if (swipe.x > 0) {
                            OnMoveRight?.Invoke();
                        }
                        else {
                            OnMoveLeft?.Invoke();
                        }
                    }
                }
            }
        }

    }
}