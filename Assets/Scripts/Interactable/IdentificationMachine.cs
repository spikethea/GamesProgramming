using UnityEngine;


public class IdentificationMachine: MonoBehaviour {
    [SerializeField] private UIManager UI;
    private GameManager Game;
    [SerializeField] PlayerMotor player;
    private Scannable scannable;

    public Convict target;

    public CaptionOnly BaseTower;
    public CaptionOnly Headquarters;

    private int reward;
    private bool hasIDBeenCollected = false;
    private Renderer renderer;
    public Material ScreenGreen;
    public Material ScreenOrange;

    private void Start()
    {
        renderer = transform.GetComponent<Renderer>();
        renderer.material = ScreenOrange;

        scannable = transform.GetComponent<Scannable>();
        Game = FindAnyObjectByType<GameManager>();
        reward = target.reward;
    }

    public void SetBountyTarget() {
        
        if (hasIDBeenCollected) {
            player.EarnCredits(reward);
            Game.currentTarget = "";
            scannable.HideFloatingText();
            scannable.promptMessage = "Bounty Complete";
            renderer.enabled = false;
            BaseTower.CaptionText = "Acquire Target";
            return;
        }

        if (Game.currentTarget != string.Empty)
        {
            UI.mainUI.SetMainText("You are already have a bounty");
            return;
        }
        Game.currentTarget = target.name;
        UI.mainUI.SetTarget(target);

        Headquarters.ShowFloatingText();
        BaseTower.HideFloatingText();
    }

    public void Preview() {
        if (Game.currentTarget != "" && target != null)
        {
            return;
        }
        UI.mainUI.PreviewTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasIDBeenCollected) return;
        if (target == null && Game.currentTarget == target.name) {
            Headquarters.HideFloatingText();
            BaseTower.CaptionText = "Collect Reward";
            BaseTower.ShowFloatingText();

            scannable.promptMessage = "Collect Reward [E]";
            scannable.Caption = "Collect Reward [E]";
            renderer.material = ScreenGreen;

            hasIDBeenCollected = true;
        }
    }
}
