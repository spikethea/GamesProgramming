using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Add or remove an InteractionEvent component to this gameobject.
    public bool useEvents;
    // message displayed to player when looking at an interactable.
    [SerializeField]
    public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }

    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        // template function to be overwrttien by subclassess
    }
}
