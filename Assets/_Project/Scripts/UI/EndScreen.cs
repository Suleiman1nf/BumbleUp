using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Suli.Bumble
{
    [Serializable]
    public class EndScreen
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private List<ParticleSystem> _endParticles;
        [SerializeField] private Button _playButton;
        
        public Action OnPlayButtonClick;

        public void Init()
        {
            _playButton.onClick.AddListener(PlayButtonClick);
        }
        
        private void SetScore(int score)
        {
            _scoreText.SetText(score.ToString());
        }

        private void PlayButtonClick()
        {
            panel.SetActive(false);
            foreach (var particle in _endParticles)
            {
                particle.Stop();
            }
            OnPlayButtonClick?.Invoke();
        }

        public void ShowEndScreen(int score)
        {
            panel.SetActive(true);   
            SetScore(score);
            foreach (var particle in _endParticles)
            {
                particle.Play();
            }
        }
    }
}