using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public string playerName;

    public float playerHealth;
    
    public string playerWeapon;

    //public Vector3 playerPosition = new Vector3(10, 5, 7); 

    public float[] playerPositionArray = new float[] { 0, 0, 0 };

    public DataToSave dataToSave = new DataToSave();

    public PlayerStats()
    {
        dataToSave = new DataToSave();
    }

    public PlayerStats(string PlayerName, string PlayerHealth, string PlayerWeapon)
    {
        playerName = PlayerName;
        if(!float.TryParse(PlayerHealth, out playerHealth))
        {
            Debug.LogError("Can't Convert Player Health to Float");
        }
        
        playerWeapon = PlayerWeapon;
    }

    public void SetPlayerPosition(Vector3 playerPos)
    {
        playerPositionArray[0] = playerPos.x;
        playerPositionArray[1] = playerPos.y;
        playerPositionArray[2] = playerPos.z;
    }

    public Vector3 ReturnPlayerPosition()
    {
        if(playerPositionArray.Length < 3)
        {
            Debug.Log("Not Elements in the array");
            return Vector3.zero;
        }
        return new Vector3(playerPositionArray[0], playerPositionArray[1], playerPositionArray[2]);
    }
    public string ReturnPlayerSaveData()
    {
        string returnData = "Name: " + playerName + ", Health: " + playerHealth + ", Weapon: " + playerWeapon;

        return playerName;
    }
}
