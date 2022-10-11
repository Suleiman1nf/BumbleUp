using UnityEngine;

namespace Suli.Bumble
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private EndScreen _endScreen;
        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private TutorialScreen _tutorialScreen;
        public EndScreen EndScreen => _endScreen;
        public ScoreUI ScoreUI => _scoreUI;

        public TutorialScreen TutorialScreen => _tutorialScreen;

        private void Awake()
        {
            _endScreen.Init();
        }


    }
}