using UnityEngine;

public class ScriptedBikeCam : MonoBehaviour
{
    public Transform thirdPersonPivot;
    public Transform firstPersonPivot;
    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = thirdPersonPivot.position;
    }
}
