using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] public Button _playButton;
        [SerializeField] public Button _exitButton;
        [SerializeField] public Button _soundButton;
        [SerializeField] public Image _soundImage;
        
        [Space(10)]
        [SerializeField] public Sprite _soundOnSprite;
        [SerializeField] public Sprite _soundOffSprite;
    }
}