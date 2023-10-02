using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : MonoBehaviour
{
    
    public GameObject winButton;
    
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
    public void ClearButton()
    {
        PlayerInput.text = null;
    }


     void Update()
    {
        if(PlayerInput.text == CorrectCode)
        {
            winButton.SetActive(true);
            
        }
    }
}
