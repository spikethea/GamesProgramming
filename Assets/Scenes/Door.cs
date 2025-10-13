using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Object;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Close()
    {
        Object.SetActive(false);
    }

    public void Open()
    {
        Object.SetActive(true);
    }
}
