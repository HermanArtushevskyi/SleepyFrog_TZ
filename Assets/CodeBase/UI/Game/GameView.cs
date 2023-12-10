using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Game
{
    public class GameView : MonoBehaviour
    {
        public GameObject HealthPanel;
        public List<GameObject> HealthSprites;
        public TextMeshProUGUI KillsCountText;
        public GameObject GameOverPanel;
        public TextMeshProUGUI KillsCountGameOverText;
    }
}