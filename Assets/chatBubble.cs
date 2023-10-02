using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chatBubble : MonoBehaviour
{
    public Transform bubble;
    public Transform camera;
    public SpriteRenderer fadeColor;
    public TextMeshProUGUI text;
    private bool shouldFade = false;

    // Start is called before the first frame update
    void Start()
    {
        bubble = GetComponent<Transform>();
        camera = GameObject.Find("SceneCamera").GetComponent<Transform>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        fadeColor = GetComponent<SpriteRenderer>();
        fadeColor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        text.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        bubble.position = new Vector3(camera.position.x - 2.47f, camera.position.y - 1.8f, camera.position.z + 9.0f);
        if(fadeColor.color.a < 1.0f && !shouldFade)
        {
            fadeColor.color = new Color(1.0f, 1.0f, 1.0f, fadeColor.color.a + 0.01f);
        }
        if(text.color.a < 1.0f && !shouldFade)
        {
            text.color = new Color(1.0f, 1.0f, 1.0f, text.color.a + 0.001f);
        }
        if(shouldFade)
        {
            fadeColor.color = new Color(1.0f, 1.0f, 1.0f, fadeColor.color.a - 0.001f);
            text.color = new Color(1.0f, 1.0f, 1.0f, text.color.a - 0.01f);
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(4.0f);
        shouldFade = true;
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
