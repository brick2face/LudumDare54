using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LemApperson.TornPaper
{
    public class BattleShipButton : MonoBehaviour
    {
        private int _buttonID;
        private bool _buttonState;
        private BattleShipGame _game;
        [SerializeField] GameObject _button1;
        [SerializeField] GameObject _button2;
        [SerializeField] GameObject _button3;

        public void SetButtonID(int ButtonID) {
                _buttonID = ButtonID;
        }

        private void OnEnable() {
            _game = GetComponentInParent<BattleShipGame>();
        }

        /// <summary>
        /// If Correct, top button graphic is turned off.
        /// If Incorrect, both top and middle buttons are turned off.
        /// </summary>
        public void ButtonClicked() {
            if (!_buttonState) {
                _buttonState = true;
                int Status = _game.ShipHitted(_buttonID);
                switch (Status)
                {
                    case 0:
                        _button3.SetActive(false);
                        _button2.SetActive(false);
                        break;
                    case 1:
                        _button3.SetActive(false);
                        break;
                    case 2:
                        break;
                }
            }
        }

        /// <summary>
        /// When a button has been played, its state becomes true.
        /// </summary>
        public void SetButtonState(bool ButtonState) {
            _buttonState = ButtonState;
        }

        /// <summary>
        /// Bring button back to its initial state.
        /// </summary>
        public void Reset() {
            _button3.SetActive(true);
            _button2.SetActive(true);
            _buttonState = false;
        }
    }
}