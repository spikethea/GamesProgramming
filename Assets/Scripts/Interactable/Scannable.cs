using UnityEngine;
using TMPro;

public class Scannable : Interactable
{
    public GameObject FloatingTextPrefab;
    public string Caption = "Default";
    private Camera _cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = Caption;
        _cam = Camera.main;

    }

    private void Update()
    {
      FloatingTextPrefab.transform.LookAt(_cam.transform.position);

    }

    // Update is called once per frame


    public void ShowFloatingText ()
    {
        FloatingTextPrefab.SetActive(true);
        Debug.Log("Show");

    }

    public void HideFloatingText()
    {
        FloatingTextPrefab.SetActive(false);
        Debug.Log("Hide");
    }

    protected override void Interact()
    {
        Debug.Log("Scannable Object Interact");
        
    }
}
