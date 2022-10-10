using System;
using UnityEngine;

namespace Suli.Bumble
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private StairsBuilder _stairsBuilder;
        [SerializeField] private Stair _stairPrefab;
        [SerializeField] private Player _player;
        private Score _score;
        private int currPlayerStair = 2;

        public void Start()
        {
            StartGame();
            _player.SetToStairPosition(_stairsBuilder.GetStairPosition(2));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _player.MoveSide(-1);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _player.MoveSide(1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                _player.MoveUp(_stairsBuilder.GetStairPosition(++currPlayerStair));
            }
        }

        public void StartGame()
        {
            _score = new Score();
            for (int i = 0; i < 15; i++)
            {
                _stairsBuilder.AddStair(_stairPrefab);
            }
        }

        public void EndGame()
        {
            
        }
    }
}