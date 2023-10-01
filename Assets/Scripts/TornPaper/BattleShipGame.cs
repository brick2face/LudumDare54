using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LemApperson.TornPaper
{
    public class BattleShipGame : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private AudioClip[] _audioClips;
        private BattleShipButton[] _buttons;
        private int[] _shipPositions;
        public int _correctAnswers, _attempts;
        private bool _gameOver;

        /// <summary>
        /// Populate the grid with 36 buttons. Each is given a unique name,
        /// and an unique ID.
        /// Game data is set
        /// </summary>
        void Start() {
            for (int i = 0; i < 36; i++) {
                GameObject _button = Instantiate(_buttonPrefab, transform);
                _button.transform.SetParent(transform);
                _button.name = "Button" + i;
                _button.GetComponent<BattleShipButton>().SetButtonID(i);
            }
            _buttons = GetComponentsInChildren<BattleShipButton>();
            PickBoardData();
        }

        private void PickBoardData()
        {
            int randomValue = Random.Range(1, 5); // Generates a random integer between 1 and 4 (inclusive)
            switch (randomValue)
            {
                case 1:
                    _shipPositions= new int[] {7,8,9,10,24,25,26,22,28,34 };
                    break;
                case 2:
                    _shipPositions= new int[] {0,6,12,18,9,10,11,27,28,29 };
                    break;
                case 3:
                    _shipPositions= new int[] {31,32,33,34,1,7,13,5,11,17 };
                    break;
                case 4:
                    _shipPositions= new int[] {26,27,28,29,13,14,15, 3,4,5 };
                    break;
                default:
                    _shipPositions= new int[] {0,6,12,18,9,10,11,28,29,30 };
                    break;
            }
        }


        public int ShipHitted(int ButtonId) {
            _attempts++; 
            if (!_gameOver) {
                return CheckIfElementsMatch(ButtonId);
            }
            return 2;
        }

        /// <summary>
        /// the button id is compared to the game data
        /// </summary>
        int CheckIfElementsMatch(int ButtonID) {    
            if (_attempts == 18) {
                _gameOver = true;
                StartCoroutine(ResetBoard());
            }
            for (int i = 0; i < _shipPositions.Length; i++) {
                if (_shipPositions[i] == ButtonID) {
                    _correctAnswers++;
                    if (_correctAnswers == 10) {
                        GameWon();
                    }     
                    if (_attempts == 18)
                    {
                        _gameOver = true;
                        StartCoroutine(ResetBoard());
                    }
                    AudioManager.Instance.PlaySFX(_audioClips[0]);
                    return 1;  // true
                }
            }
            AudioManager.Instance.PlaySFX(_audioClips[1]);
            return 0;  // false
        }

        /// <summary>
        /// When there have been 10 correct guesses, the game is won.
        /// The buttons are set to true to make them inactive.
        /// </summary>
        private void GameWon() {
            AudioManager.Instance.PlaySFX(_audioClips[2]);
           _gameOver = true;
           for (int i = 0; i < _buttons.Length; i++) {
               _buttons[i].SetButtonState(true);
           }
           GameManager.Instance.SetGameStoryVariable("BattleShipPuzzle", true);
           // StartCoroutine(ResetBoard());
        }

        private IEnumerator ResetBoard()
        {
            yield return new WaitForSeconds(7f);
            for (int i = 0; i < _buttons.Length; i++) {
                _buttons[i].Reset();
            }
            PickBoardData();
            _correctAnswers = 0;
            _attempts = 0;
            _gameOver = false;
        }
    }
}

//
//  0       1       2       3       4       5
//  6       7       8       9       10      11
//  12      13      14      15      16      17
//  18      19      20      21      22      23
//  24      25      26      27      28      29
//  30      31      32      33      34      35
//

//  Ship1  26, 27,28,29
//  Ship2  13,14,15
//  Ship3  3,4,5