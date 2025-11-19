using System;
using UnityEngine;

public class FSMCannon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Definition of a new type
    public enum State
    {
        Aiming,
        Charging,
        Waiting
    }

    public State CurrentState = State.Aiming;

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case State.Aiming:
                Aiming();

                if (IsSpacePressed())
                {
                    StartCharging();
                    CurrentState = State.Charging;
                }
                break;
            case State.Charging:
                Charging();
                if (IsSpaceReleased()) {
                    FireProjectile();
                    CurrentState = State.Waiting;
                }
                break;
            case State.Waiting:
                Waiting();
                if (IsReadyToShootAgain())
                {
                    StopWaiting();
                    CurrentState = State.Aiming;
                }
                break;
        }
    }

    #region Waiting
    private void StopWaiting()
    {
        throw new NotImplementedException();
    }


    private bool IsReadyToShootAgain()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Aiming
    [Header("Horizontal Rotation")]
    [Range(0f, 1f)]
    public float H = 0.5f; // t value in lerping for horizontal rotation
    public Transform BarrelH;
    public Transform H0;
    public Transform H1;
    public float RotationHSpeed = 1f;

    [Header("Vertical Rotation")]
    [Range(0f, 1f)]
    public float V = 0.5f; // t value in lerping for horizontal rotation
    public Transform BarrelV;
    public Transform V0;
    public Transform V1;
    public float RotationVSpeed = 1f;
    void Aiming()
    {
        H += Input.GetAxis("Horizontal") * Time.deltaTime * RotationHSpeed; // per frame -> per second
        H = Mathf.Clamp01(H);

        BarrelH.localRotation = Quaternion.Slerp(H0.localRotation, H1.localRotation, H);

        V += Input.GetAxis("Vertical") * Time.deltaTime * RotationVSpeed; // per frame -> per second
        V = Mathf.Clamp01(V);

        BarrelV.localRotation = Quaternion.Slerp(V0.localRotation, V1.localRotation, V);



        //float minAngle = -45f;
        //float maxANgle = +45f;
        //Vector3 localEulerAngles = BarrelH.localEulerAngles;
        //localEulerAngles.z = Mathf.Lerp(minAngle, maxANgle, H);

        //BarrelH.localEulerAngles = localEulerAngles;
    }
     
    private void StartCharging()
    {
        throw new NotImplementedException();
    }
    private bool IsSpacePressed() => Input.GetKeyDown(KeyCode.Space);

    #endregion

    #region Charging


    private void FireProjectile()
    {
        throw new NotImplementedException();
    }

    void Charging()
    {
        throw new NotImplementedException();
    }


    private bool IsSpaceReleased() => Input.GetKeyUp(KeyCode.Space);

    #endregion

    #region Waiting
    void Waiting()
    {
        throw new NotImplementedException();
    }
    #endregion


}
