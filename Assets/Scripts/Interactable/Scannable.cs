using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Scannable : Interactable
{
    public GameObject FloatingTextPrefab;
    private GameObject _floatingTextInstance;
    public string Caption = "Default";
    private Camera _cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Vector3 pos = new Vector3(0f, 5f, 0f);
        _floatingTextInstance = Instantiate(FloatingTextPrefab, this.transform.position + pos, this.transform.rotation);
        _floatingTextInstance.GetComponentInChildren<TextMeshPro>().text = Caption;
        HideFloatingText();
        _floatingTextInstance.transform.parent = transform;
        _cam = Camera.main;

    }

    private void Update()
    {
        var rot = Quaternion.LookRotation(_floatingTextInstance.transform.position - _cam.transform.position);
        _floatingTextInstance.transform.rotation = rot;

    }

    // Update is called once per frame


    public void ShowFloatingText ()
    {
        _floatingTextInstance.SetActive(true);
        Debug.Log("Show");

    }

    public void HideFloatingText()
    {
        _floatingTextInstance.SetActive(false);
        Debug.Log("Hide");
    }

    protected override void Interact()
    {
        Debug.Log("Scannable Object Interact");
        
    }
}
