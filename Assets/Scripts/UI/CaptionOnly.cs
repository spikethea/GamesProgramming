using TMPro;
using UnityEngine;

public class CaptionOnly : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    private GameObject _floatingTextInstance;
    public string CaptionText = "Default";
    public float height = 5f;
    public float size = 1f;
    public bool isDefaultVisible = true;
    private Camera _cam;
    private void Awake()
    {
        Vector3 pos = new Vector3(0f, height*size, 0f);
        _floatingTextInstance = Instantiate(FloatingTextPrefab, this.transform.position + pos, this.transform.rotation);
        _floatingTextInstance.transform.localScale = new Vector3(size, size, size);
        _floatingTextInstance.GetComponentInChildren<TextMeshPro>().text = CaptionText;
        _floatingTextInstance.transform.parent = transform;
        _cam = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!isDefaultVisible)
            HideFloatingText();
    }



    // Update is called once per frame
    void Update()
    {
        var rot = Quaternion.LookRotation(_floatingTextInstance.transform.position - _cam.transform.position);
        _floatingTextInstance.transform.rotation = rot;
    }

    public void ShowFloatingText()
    {
        _floatingTextInstance.SetActive(true);
        Debug.Log("Show");

    }

    public void HideFloatingText()
    {
        _floatingTextInstance.SetActive(false);
        Debug.Log("Hide");
    }

    public void UpdateCaptionText (string newText)
    {
        _floatingTextInstance.GetComponentInChildren<TextMeshPro>().text = newText;
    }
}
