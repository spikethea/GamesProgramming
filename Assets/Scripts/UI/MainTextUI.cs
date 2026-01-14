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
        CentreText.text = prompt;
    }

    public void ClearTitleScreen() {
        CentreText.color = Color.white;
        CentreText.text = "";
    }




    public void SetTarget(Convict target)
    {
        BountyText.text = $"Target: {target.name}\nLocation: {target.location}";
    }

    public void CapturedTarget() {
        BountyText.text = "";
        CentreText.text = "Bountry Completed";
        Invoke(nameof(ClearCentreText), 3f);
    }

    public void PreviewTarget(Convict target)
    {
        CentreText.text = $"Name: {target.name} \n Wanted For: {target.crime} \n Last Seen: {target.location} \n\n Reward: ${target.reward}";
    }

    public void ClearCentreText() {
        CentreText.text = "";
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
