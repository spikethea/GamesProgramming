using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float interactableDistance = 3f;
    private float distance = 30f;
    [SerializeField]
    private LayerMask mask;
    [SerializeField] private UIManager UI;
    public InputManager inputManager;
    private Interactable lastHit;
    private Scannable lastHitScan;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // create a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            Debug.Log(hitInfo.collider.name);
            //Ray Hitting Interactables
            if (hitInfo.distance < interactableDistance)
            {
                
                // At the shorter interactableDistance certain (Interactable) objects are applicable
                if (hitInfo.collider.GetComponent<Interactable>() != null)
                {
                    Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                    UI.mainUI.SetPrompt(hitInfo.collider.GetComponent<Interactable>().promptMessage);
                    lastHit = interactable;
                    if (inputManager.onFoot.Interact.triggered)
                    {
                        interactable.BaseInteract();
                    }
                }

            }
            else
            // Ray Hitting Scannalbe objects
            {
                // otherwise, only Scannable objects apply
                if (hitInfo.collider.GetComponent<Scannable>() != null)
                {
                    Scannable scannable = hitInfo.collider.GetComponent<Scannable>();
                    Debug.Log(hitInfo.collider.name);
                    scannable.ShowFloatingText();
                    lastHitScan = scannable;
                    if (inputManager.onFoot.Interact.triggered)
                    {
                        scannable.BaseInteract();
                    }

                    if (scannable.GetComponent<IdentificationMachine>()) {
                        scannable.GetComponent<IdentificationMachine>().Identify();
                    }
                }
            }
            Debug.Log(hitInfo.collider);
            // Ray Hitting Non-Player Characters
            if (hitInfo.collider.GetComponent<NPC>() != null) {
                NPC npc = hitInfo.collider.GetComponent<NPC>();
                PlayerMotor motor = GetComponent<PlayerMotor>();
                if (inputManager.isAiming & !motor.ScannerIsEquipped) {
                    Debug.Log("Aiming Gun at NPC");
                    npc.isPlayerAimingatMe = true;
                }
            }
        }
        else
        {
            
            if (lastHit != null)
            {
                Debug.Log("Empty Prompt");
                UI.mainUI.SetPrompt("");
                UI.mainUI.SetMainText("");
                lastHit = null;
            }

            // Hide the text of the last Scannable object which the raycaster left and destroy textMesh
            if (lastHitScan != null)
            {
                lastHitScan.HideFloatingText();
                if (lastHitScan.GetComponent<IdentificationMachine>() != null)
                {
                    UI.mainUI.ClearText();
                }
                lastHitScan = null;
            }


        }

        



    }
}
