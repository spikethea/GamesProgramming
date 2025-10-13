using UnityEngine;

public class MyDoorTrigger : MonoBehaviour
{
    public Door Door;

    public KeyCode OpenKey = KeyCode.E;
    public KeyCode CloseKey = KeyCode.Q;
    private bool PlayerIsInside = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            PlayerIsInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsInside = false;
        }
    }

    private void Update()
    {
        if (!PlayerIsInside) return;
        Debug.Log("Player inside area");
        if (Input.GetKeyDown(OpenKey))
        {
            Door.Open();
        }

        if (Input.GetKeyDown(CloseKey))
        {
            Door.Close();
        }
    }
}
