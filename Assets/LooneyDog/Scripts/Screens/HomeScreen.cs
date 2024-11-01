using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{
    public class HomeScreen : MonoBehaviour
    {

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        [Header("Properties")]
        [SerializeField] private float _transitionSpeed;

        public Button StartButton { get => _startButton; set => _startButton = value; }
        public Button ContinueButton { get => _continueButton; set => _continueButton = value; }
        public Button ExitButton { get => _exitButton; set => _exitButton = value; }

        private void Awake()
        {
            _startButton.onClick.AddListener(OnClickStart);
            _continueButton.onClick.AddListener(OnClickContinue);
            _exitButton.onClick.AddListener(OnClickExit);
        }

        private void OnClickStart() {
            GameManager.Game.Screen.Load.LoadLevel(2, GameDifficulty.Easy, gameObject);
        }
        private void OnClickContinue() { }
        private void OnClickExit() { }

    }

}