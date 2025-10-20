using UnityEditor.PackageManager;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [Header("Laser")]
    public Transform FirePoint;

    [Range(0, 10)]
    public float MaxDistance = 10.0f;
    public LayerMask Mask;

    [Header("Rendering")]
    public LineRenderer Line;

    public float Radius = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(FirePoint.position, FirePoint.forward, out hitInfo, MaxDistance);
            if (hit)
            {
                DrawLaser(hitInfo.point);
            }
            else
            {
                DrawLaser(FirePoint.position + FirePoint.forward * MaxDistance);
            }
            Line.enabled = true;
        }
        else {
            Line.enabled = false;
        }
    }

    private void DrawLaser(Vector3 endPoint)
    {
        Line.SetPosition(0, FirePoint.position);
        Line.SetPosition(1, FirePoint.position + endPoint / 2f + Random.insideUnitSphere * Radius);
        Line.SetPosition(2, endPoint);
    }
}
