using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScene : MonoBehaviour
{
    void Awake()
    {
        // Fade in from black. To avoid flicker, we set the color to black and then fade in.
        Image _fp = UIManager.Instance.m_FadePanel.GetComponent<Image>();
        _fp.color = new Color32((byte)0.0f, (byte)0.0f, (byte)0.0f, (byte)255.0f);
        UIManager.Instance.m_FadePanel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.FadeFromBlack(2.0f);
    }

}
