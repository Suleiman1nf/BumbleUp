using System;
using UnityEngine;
using UnityEngine.UI;

namespace Suli.Bumble
{
    [Serializable]
    public class TutorialScreen
    {
        [SerializeField] private GameObject moveTutorialPanel;
        [SerializeField] private GameObject sideTutorialPanel;

        public void ShowMovePanel(bool state)
        {
            moveTutorialPanel.SetActive(state);
        }

        public void ShowSidePanel(bool state)
        {
            sideTutorialPanel.SetActive(state);
        }
    }
}