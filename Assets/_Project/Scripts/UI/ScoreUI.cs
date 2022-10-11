using System;
using TMPro;
using UnityEngine;

namespace Suli.Bumble
{
    [Serializable]
    public class ScoreUI
    {
        [SerializeField] private TMP_Text scoreText;

        public void UpdateScore(int score)
        {
            scoreText.SetText(score.ToString());
        }
    }
}