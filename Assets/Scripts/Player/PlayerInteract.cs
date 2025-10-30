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
    private PlayerUI playerUI;
    private InputManager inputManager;
    public Scannable lastHit;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        // create a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.distance < interactableDistance)
            {
                // At the shorter interactableDistance certain (Interactable) objects are applicable
                if (hitInfo.collider.GetComponent<Interactable>() != null)
                {
                    Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                    playerUI.UpdateText(hitInfo.collider.GetComponent<Interactable>().promptMessage);
                    if (inputManager.onFoot.Interact.triggered)
                    {
                        interactable.BaseInteract();
                    }
                }

            }
            else
            {
                // otherwise, only Scannable objects apply
                if (hitInfo.collider.GetComponent<Scannable>() != null)
                {
                    Scannable scannable = hitInfo.collider.GetComponent<Scannable>();
                    scannable.ShowFloatingText();
                    lastHit = scannable;
                    if (inputManager.onFoot.Interact.triggered)
                    {
                        scannable.BaseInteract();
                    }
                } else 
                {
                    // Hide the text of the last Scannable object which the raycaster left and destroy textMesh
                    if (lastHit != null)
        
                    {
                        lastHit.HideFloatingText();
                        lastHit = null;
                    }
                }
            }

        }

        

    }
}
