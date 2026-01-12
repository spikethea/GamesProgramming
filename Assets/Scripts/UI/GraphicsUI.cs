using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class GraphicsUI : MonoBehaviour
{
    public Image damageFilter;
    public Image scannerFilter;

    private Coroutine fadeCoroutine = null;
    public float fadeSpeed = 0.5f;
    private Color damageColor;
    private Color color;
    private Color scannerColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageColor = damageFilter.color;
        color = damageFilter.color;

        scannerColor = scannerFilter.material.color;

        damageFilter.color = new Color(1, 1, 1, 0f);
        scannerFilter.color = new Color(1, 1, 1, 0f);
    }

    public void CriticalHealth() {
        damageFilter.color = new Color(1, 1, 1, 0.6f);
    }

    public void ResetGraphicsUI()
    {
        damageFilter.color = new Color(1, 1, 1, 0f);
        scannerFilter.color = new Color(1, 1, 1, 0f);
    }

    public void StartFade() {
        fadeCoroutine = StartCoroutine(Fade(1));
    }

    IEnumerator Fade(float startAlpha)
    {
        damageColor = damageFilter.color;
        damageColor.a = startAlpha;
        damageFilter.color = damageColor;
        damageFilter.color = damageColor;

        while (damageColor.a > 0)
        {
            damageColor.a -= fadeSpeed * Time.deltaTime;
            damageFilter.color = damageColor;
            yield return null;
        }

        yield return 0;
    }

    public void ShowScanner()
    {
        //scannerFilter.enabled = true;
        scannerColor = new Color(1, 1, 1, 1f);
        scannerFilter.color = scannerColor;
    }

    public void HideScanner()
    {
        //scannerFilter.enabled = false;
        scannerColor = new Color(1, 1, 1, 0f);
        scannerFilter.color = scannerColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
