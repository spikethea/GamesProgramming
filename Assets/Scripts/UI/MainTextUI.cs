using UnityEngine;
using TMPro;

public class MainTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CentreText;
    [SerializeField] private TextMeshProUGUI PromptText;
    [SerializeField] private TextMeshProUGUI BountyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {

    }

    public void SetPrompt(string prompt)
    {
        PromptText.text = prompt;
    }

    public void SetMainText(string prompt)
    {
        PromptText.text = prompt;
    }




    public void SetTarget(string name, string crime, string location, int reward)
    {
        CentreText.text = $"Name: {name} \n Wanted For: {crime} \n Last Seen: {location} \n\n Reward: ${reward}";
        BountyText.text = $"Target: {name}\nLocation: {location}";
    }

    public void ClearText()
    {
        BountyText.text = "";
        CentreText.text = "";
        PromptText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
