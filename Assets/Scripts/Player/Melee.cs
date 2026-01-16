using UnityEngine;

public class Melee : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other != null) {
            NPC nPC = other.GetComponent<NPC>();
            if (nPC != null)
            {
                nPC.takeDamage(2);
                nPC.isPlayerShootingatMe = true;
            }
        }
    }
}
