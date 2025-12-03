using UnityEngine;

public struct Target
{
    public string name;
    public string crime;
    public string location;
    public int reward;
}
public class IdentificationMachine: MonoBehaviour {
    public UIManager UI;
    public GameManager Game;
    [SerializeField] private string Name = "Carla";
    [SerializeField] private string Crime = "Robbery";
    [SerializeField] private string Location = "ws-40a";
    [SerializeField] private int Reward = 300;
    public Target target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        target.name = Name;
        target.crime = Crime;
        target.location = Location;
        target.reward = Reward;
    }

    public void Identify() {
        Game.currentTarget = target;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
