using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NovelPageManager : MonoBehaviour
{
    [SerializeField]
    private Button m_NextButton;

    public string DestinationSceneName;

    // Start is called before the first frame update
    void Start()
    {
        m_NextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(DestinationSceneName);
        });
    }
}
