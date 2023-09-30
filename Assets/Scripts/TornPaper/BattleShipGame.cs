using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LemApperson.TornPaper
{
    public class BattleShipGame : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private AudioClip[] _audioClips;
        private int[] _shipPositions;
        private int _correctAnswers;
        private bool _gameWon;

        void Start() {
            for (int i = 0; i < 36; i++) {
                GameObject _button = Instantiate(_buttonPrefab, transform);
                _button.transform.parent = transform;
                _button.name = "Button" + i;
                _button.GetComponent<BattleShipButton>().SetButtonID(i);
            }
            _shipPositions= new int[] {7,8,9,10,24,25,26,22,28,34 };
        }

        public bool ShipHitted(int ButtonId) {
            if (!_gameWon) {
                return CheckIfElementsMatch(ButtonId);
            }
            return false;
        }

        bool CheckIfElementsMatch(int ButtonID) {
            for (int i = 0; i < _shipPositions.Length; i++) {
                if (_shipPositions[i] == ButtonID) {
                    _correctAnswers++;
                    if (_correctAnswers == 10) {
                        GameWon();
                    }
                    AudioManager.Instance.PlaySFX(_audioClips[0]);
                    return true;
                }
            }
            AudioManager.Instance.PlaySFX(_audioClips[1]);
            return false;
        }

        private void GameWon() {
            // GameManager.Instance.GameWon();
           _gameWon = true;
        }
    }
}

//
//  0       1       2       3       5       6
//  6       8       9       10      11      12
//  13      14      15      16      17      18
//  19      20      21      22      23      24
//  25      26      27      28      29      30
//  31      32      33      34      35      36
//

//  Ship1  8,9,10,11
//  Ship2  25,26,27
//  Ship3  23,29,35