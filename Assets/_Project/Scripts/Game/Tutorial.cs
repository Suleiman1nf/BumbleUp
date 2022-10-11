using System.Collections;
using UnityEngine;

namespace Suli.Bumble
{
    public class Tutorial
    {
        private bool _isPlayerMovedSide = false;
        private bool _isPlayerMovedUp = false;
        private readonly TutorialScreen _tutorialScreen;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly PlayerInput _playerInput;

        public Tutorial(TutorialScreen tutorialScreen, ICoroutineRunner coroutineRunner, PlayerInput playerInput)
        {
            _tutorialScreen = tutorialScreen;
            _coroutineRunner = coroutineRunner;
            _playerInput = playerInput;

            _playerInput.OnMoveLeft += () => _isPlayerMovedSide = true;
            _playerInput.OnMoveRight += () => _isPlayerMovedSide = true;
            _playerInput.OnMoveUp += () => _isPlayerMovedUp = true;
        }

        public void StartTutorial()
        {
            _coroutineRunner.StartCoroutine(StartTutorialCoroutine());
        }
        
        private IEnumerator StartTutorialCoroutine()
        {
            yield return _coroutineRunner.StartCoroutine(MoveTutorial());
            _coroutineRunner.StartCoroutine(SideTutorial());
        }

        private IEnumerator MoveTutorial()
        {
            _tutorialScreen.ShowMovePanel(true);
            yield return new WaitUntil(()=>_isPlayerMovedUp);
            _tutorialScreen.ShowMovePanel(false);
        }

        private IEnumerator SideTutorial()
        {
            _tutorialScreen.ShowSidePanel(true);
            yield return new WaitUntil(()=>_isPlayerMovedSide);
            _tutorialScreen.ShowSidePanel(false);
        }


    }
}