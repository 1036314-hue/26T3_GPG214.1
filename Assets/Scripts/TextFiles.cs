using System.IO;
using UnityEngine;

public class TextFiles : MonoBehaviour
{
    public PlayerStats currentSaveData = new PlayerStats();

    public string fileName = "CharacterSaveData.txt";

    public string textFileContent;

    private void Start()
    {
        currentSaveData = new PlayerStats("Jeffrey", "69", "Gun");

        
        WriteData(currentSaveData.ReturnPlayerSaveData());
    }

    void GetTextFileContent()
    {
        if(!string.IsNullOrEmpty(fileName))
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if(File.Exists(filePath))
            {
                textFileContent = File.ReadAllText(filePath);
                Debug.Log(textFileContent);
            }
            else
            {
                Debug.Log("File Does Not Exist");
            }
        }
        
    }

    void WriteData(string dataToWrite)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        using(StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(dataToWrite);
        }
    }
}
