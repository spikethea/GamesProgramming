using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    // Player Stat UI Elements
    [SerializeField] private TextMeshProUGUI WeaponSwapUI;
    [SerializeField] private TextMeshProUGUI HealthUI;
    [SerializeField] private TextMeshProUGUI CreditsUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Restart ()
    {
        WeaponSwapUI.text = "";
        UpdateHealth(3);
        UpdateCredits(0);
    }

    public void UpdateHealth(int currentHealth)
    {
        string tmp = "";
        for (int i = 0; i < currentHealth; i++)
        {
            tmp += "/ ";
        }
        HealthUI.text = $"Health: {tmp}";
       
    }

    public void UpdateCredits(int currentCredits)
    {
        CreditsUI.text = $"$ {currentCredits}/900";
        StartCoroutine(AnimateCreditChange());
    }

    IEnumerator AnimateCreditChange()
    {
        var numberOfFlashes = 0;
        while (numberOfFlashes > 3) {
            CreditsUI.color = Color.grey;
            yield return new WaitForSeconds(0.3f);
            CreditsUI.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }

        yield return null;

    }

    public void isWeaponEquipped(bool weaponEquipped)
    {
        if (weaponEquipped) {
            WeaponSwapUI.text = "Gun <\n <color=#808080>Scanner [Q]</color>";
        } else {
            WeaponSwapUI.text = "Scanner <\n <color=#808080>Gun [Q] </color>";
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
