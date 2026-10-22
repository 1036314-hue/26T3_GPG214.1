using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [SerializeField]
    private string playerName;

    [SerializeField]
    private float playerHealth;
    
    [SerializeField]
    private string playerWeapon;

    public PlayerStats()
    {

    }

    public PlayerStats(string playerName, string playerHealth, string playerWeapon)
    {
        this.playerName = playerName;
        if(float.TryParse(playerHealth, out this.playerHealth))
        {
            Debug.LogError("Can't Convert Player Health to Float");
        }
        
        this.playerWeapon = playerWeapon;
    }

    public string ReturnPlayerSaveData()
    {
        string returnData = "Name: " + playerName + "Health: " + playerHealth + "Weapon: " + playerWeapon;

        return playerName;
    }
}
