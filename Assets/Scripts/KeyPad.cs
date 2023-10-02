using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : MonoBehaviour
{
    /*
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
    public Button Button6;
    public Button Button7;
    public Button Button8;
    public Button Button9;*/
    public TMP_InputField PlayerInput;
    public string CorrectCode;

    public void ButtonPress1()
    {
        PlayerInput.text = PlayerInput.text + "1";
    }
    public void ButtonPress2()
    {
        PlayerInput.text = PlayerInput.text + "2";
    }
    public void ButtonPress3()
    {
        PlayerInput.text = PlayerInput.text + "3";
    }
    public void ButtonPress4()
    {
        PlayerInput.text = PlayerInput.text + "4";
    }
    public void ButtonPress5()
    {
        PlayerInput.text = PlayerInput.text + "5";
    }
    public void ButtonPress6()
    {
        PlayerInput.text = PlayerInput.text + "6";
    }
    public void ButtonPress7()
    {
        PlayerInput.text = PlayerInput.text + "7";
    }
    public void ButtonPress8()
    {
        PlayerInput.text = PlayerInput.text + "8";
    }
    public void ButtonPress9()
    {
        PlayerInput.text = PlayerInput.text + "9";
    }

    void Confirm()
    {
       if(PlayerInput.text==CorrectCode)
        {
            //win
        }
        else
        {
            PlayerInput.text = null;
        }
    }
}
