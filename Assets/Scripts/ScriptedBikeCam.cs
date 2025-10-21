using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScriptedBikeCam : MonoBehaviour
{
    public Transform thirdPersonPivot;
    public Transform firstPersonPivot;
    private Transform orientation;
    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = thirdPersonPivot.position;
        orientation.forward = viewDir.normalized;
    }
}
