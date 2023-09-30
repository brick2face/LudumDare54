using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This event is fired when the fade is finished. Bool is true if fading to black, false if fading from black.
/// </summary>
public class FadeFinishedEvent : UnityEngine.Events.UnityEvent { }

public class UIManager : MonoBehaviour
{
    #region SINGLETON PATTERN 
    private static int m_referenceCount = 0;

    private static UIManager m_instance;

    public static UIManager Instance
    {
        get
        {
            return m_instance;
        }
    }


#pragma warning disable IDE0051

    void Awake()
    {
        m_referenceCount++;
        if (m_referenceCount > 1)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        m_instance = this;
        // Use this line if you need the object to persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }


    void OnDestroy()
    {
        m_referenceCount--;
        if (m_referenceCount == 0)
        {
            m_instance = null;
        }

    }

#pragma warning restore IDE0051
    #endregion

    // Events
    public FadeFinishedEvent OnFadeFinished = new FadeFinishedEvent();

    // GameObjects
    [SerializeField]
    public GameObject m_FadePanel;

    /// <summary>
    /// This function will fade the screen to black over the specified duration.
    /// </summary>
    /// <param name="duration">How long the fade should take.</param>
    public void FadeToBlack(float duration)
    {
        if (m_FadePanel != null)
        {
            m_FadePanel.SetActive(true);
            StartCoroutine(PerformFade(duration, true));
        }
    }

    /// <summary>
    /// This function will fade the screen from black to the camera over the specified duration.
    /// </summary>
    /// <param name="duration">How long the fade should take.</param>
    public void FadeFromBlack(float duration)
    {
        if (m_FadePanel != null)
        {
            m_FadePanel.SetActive(true);
            StartCoroutine(PerformFade(duration, false));
        }
    }

    /// <summary>
    /// This function is called when the fade is finished.
    /// </summary>
    /// <param name="duration">How long the fade took.</param>
    /// <param name="isFadeToBlack">True if we are fading to black, or false if we are fading from black to a scene.</param>
    /// <returns></returns>
    IEnumerator PerformFade(float duration, bool isFadeToBlack)
    {
        float t = 0f;
        float speed = 2.0f;

        while (t < duration)
        {
            t += Time.deltaTime * speed;
            Image fadeImage = m_FadePanel.GetComponent<Image>();
            Color32 a = isFadeToBlack ? new Color32(0, 0, 0, (byte)0) : new Color32(0, 0, 0, (byte)255);
            Color32 b = isFadeToBlack ? new Color32(0, 0, 0, (byte)255) : new Color32(0, 0, 0, (byte)0);
            Color32 lerpedColor = Color32.Lerp(a, b, t / duration);
            fadeImage.color = lerpedColor;
            yield return null;
        }

        // Wait another second for the fade to finish / decent UX.
        yield return new WaitForSeconds(1.0f);

        if (isFadeToBlack) OnFadeFinished.Invoke();
    }

}
