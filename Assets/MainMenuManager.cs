using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button m_StartButton;
    [SerializeField]
    private Button m_LoadButton;
    [SerializeField]
    private Button m_CreditsButton;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.HasSavedGame())
        {
            m_LoadButton.interactable = true;
        }
        else
        {
            m_LoadButton.interactable = false;
        }

        m_StartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartNewGame();
        });

        m_LoadButton.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadSavedGame();
        });

        m_CreditsButton.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadScene("Credits");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
