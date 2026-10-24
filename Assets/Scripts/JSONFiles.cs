using System.IO;
using UnityEngine;

public class JSONFiles : MonoBehaviour
{
    public PlayerStats stats = new PlayerStats();
    public string playerJSONFileName = "Player.json";
    public string folderPath = Application.streamingAssetsPath;
    private string fullFilePath = string.Empty;

    private void Start()
    {
       fullFilePath = Path.Combine(folderPath, playerJSONFileName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveJSON();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            LoadJSON();
        }
    }

    void SaveJSON()
    {
        stats.SetPlayerPosition(transform.position);

        string jsonData = JsonUtility.ToJson(stats);

        File.WriteAllText(fullFilePath, jsonData);
    }

    void LoadJSON()
    {
        if(File.Exists(fullFilePath))
        {
            string jsonData = File.ReadAllText(fullFilePath);

            stats = JsonUtility.FromJson<PlayerStats>(jsonData);

            if(stats != null)
            {
                Debug.Log("Player Save Position: " + stats.ReturnPlayerPosition());
                transform.position = stats.ReturnPlayerPosition();
            }
            else
            {
                Debug.LogError("JSON can't convert to class");
            }
        }
        else
        {
            Debug.LogError("No Save Data Found");
        }
    }
}
