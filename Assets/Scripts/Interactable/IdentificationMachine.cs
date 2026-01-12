using UnityEngine;


public class IdentificationMachine: MonoBehaviour {
    public UIManager UI;
    public GameManager Game;
    public Convict target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {

    }

    public void SetBountyTarget() {
        Game.currentTarget = target;
        UI.mainUI.SetTarget(target);
    }

    public void Preview() {
        UI.mainUI.PreviewTarget(target);
    }

    public void Identify()
    {
        Game.currentTarget = target;
        UI.mainUI.SetTarget(target);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
