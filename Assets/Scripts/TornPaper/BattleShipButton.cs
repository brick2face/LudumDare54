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

        public void ButtonClicked() {
            if (!_buttonState) {
                _buttonState = true;
                if (_game.ShipHitted(_buttonID)) {
                    _button3.SetActive(false);
                }  else {
                    _button3.SetActive(false);
                    _button2.SetActive(false);
                }
            }
        }

        public void SetButtonState(bool ButtonState) {
            _buttonState = ButtonState;
        }
    }
}