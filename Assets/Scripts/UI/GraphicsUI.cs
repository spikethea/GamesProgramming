using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsUI : MonoBehaviour
{
    public RawImage damageFilter;

    private Coroutine fadeCoroutine = null;
    public float fadeSpeed = 0.5f;
    private Material mat;
    private Color color;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = damageFilter.material;
        color = damageFilter.color;

        damageFilter.material.color = new Color(1, 1, 1, 0);
    }

    public void StartFade() {
        fadeCoroutine = StartCoroutine(Fade(1));
    }

    IEnumerator Fade(float startAlpha)
    {
        color = mat.color;
        color.a = startAlpha;
        mat.color = color;

        while (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            mat.color = color;
            yield return null;
        }

        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
