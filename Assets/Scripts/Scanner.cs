using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide();
    }

    public void Show()
    {
        mesh.enabled = true;
    }

    public void Hide()
    {
        mesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mesh.enabled) {
        
        }
    }
}
