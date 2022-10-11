using System;
using System.Collections;
using UnityEngine;

namespace Suli.Bumble
{
    public class Game : MonoBehaviour, IPlayerStairProvider, ICoroutineRunner
    {
        [SerializeField] private UI _ui;
        [SerializeField] private StairsBuilder _stairsBuilder;
        [SerializeField] private EnemyFabric _enemyFabric;
        [SerializeField] private Player _player;

        private IPlayerInput _playerInput;
        private Tutorial _tutorial;
        private Score _score;
        private bool _isGameStarted;
        private int _currPlayerStair = 4;

        public int PlayerStair => _currPlayerStair;


        private void Start()
        {
            Application.targetFrameRate = 60;
            Setup();
            SetPlayerToStart();
            StartGameWithTutorial();
        }

        private void Setup()
        {
            _score = new Score();
            _enemyFabric.Init(_stairsBuilder, this);
            _player.OnDie += EndGame;
            _stairsBuilder.CreateStartStairs();
            _ui.EndScreen.OnPlayButtonClick += RestartGame;
            _score.OnScoreChanged += _ui.ScoreUI.UpdateScore;
            
#if UNITY_EDITOR
            _playerInput = new KeyboardPlayerInput();
#else
            _playerInput = new GesturePlayerInput();
#endif
            _playerInput.OnMoveRight += ()=>
            {
                if(_isGameStarted)
                    _player.MoveSide(-1);
            };
            _playerInput.OnMoveLeft += () =>
            {
                if(_isGameStarted)
                    _player.MoveSide(1);
            };
            _playerInput.OnMoveUp += ()=>
            {
                if(_isGameStarted)
                    OnPlayerMove();
            };
            _tutorial = new Tutorial(_ui.TutorialScreen, this, _playerInput);
        }

        private void Update()
        {
            _playerInput.Update();
        }

        private void SetPlayerToStart()
        {
            _player.gameObject.SetActive(true);
            _player.SetToStairPosition(_stairsBuilder.GetStairPosition(_currPlayerStair));
            _player.SetToStartX();
        }

        private void OnPlayerMove()
        {
            _stairsBuilder.CreateRandomStair();
            _stairsBuilder.DisableLastStair();
            _player.MoveUp(_stairsBuilder.GetStairPosition(++_currPlayerStair));
            _score.Count++;
        }

        private IEnumerator StartCreateEnemies()
        {
            yield return new WaitWhile(() => PlayerStair < 10);
            _enemyFabric.DeleteAllEnemies();
            _enemyFabric.CreateEnemy();
        }

        private void StartGameWithTutorial()
        {
            _isGameStarted = true;
            _score.Count = 0;
            SetPlayerToStart();
            StartCoroutine(StartCreateEnemies());
            _tutorial.StartTutorial();
        }

        private void RestartGame()
        {
            _stairsBuilder.ReplaceStairs(PlayerStair);
            _isGameStarted = true;
            _score.Count = 0;
            SetPlayerToStart();
            _enemyFabric.DeleteAllEnemies();
            _enemyFabric.CreateEnemy();
        }

        private void EndGame()
        {
            _isGameStarted = false;
            _player.gameObject.SetActive(false);
            StartCoroutine(ShowUI());
        }

        private IEnumerator ShowUI()
        {
            yield return new WaitForSeconds(1.5f);
            _ui.EndScreen.ShowEndScreen(_score.Count);
        }
    }
}