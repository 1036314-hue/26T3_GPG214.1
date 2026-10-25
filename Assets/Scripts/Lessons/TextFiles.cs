using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextFiles : MonoBehaviour
{
    public PlayerStats currentSaveData = new PlayerStats();

    public string fileName = "CharacterSaveData.txt";

    public string textFileContent;

    private void Start()
    {
        //currentSaveData = new PlayerStats("Jeffrey", "69", "Gun");
        //Debug.Log(currentSaveData.ReturnPlayerSaveData());
        //WriteData(currentSaveData.ReturnPlayerSaveData());

        //GetTextFileContent();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            WriteData(currentSaveData.ReturnPlayerSaveData());
        }
        if(Input.GetKey(KeyCode.L))
        {
            GetTextFileContent();
            if(!string.IsNullOrEmpty(textFileContent))
            {
                ParseCharacterInfo(textFileContent);
            }
        }
    }

    void ParseCharacterInfo(string characterInfo)
    {
        string[] valuesFromText = characterInfo.Split(new char [] { ':', ',' },System.StringSplitOptions.RemoveEmptyEntries);

        if(valuesFromText.Length % 2 != 0 )
        {
            Debug.LogError("Something went wrong, there is an uneven amount of data");
            return;
        }

        Dictionary<string, string> characterInfoDictionary = new Dictionary<string, string>();

        for(int i= 0; i < valuesFromText.Length; i+=2)
        {
            string currentKey = valuesFromText[i].Trim();
            string currentValue = valuesFromText[i+1].Trim();
            Debug.Log("Key " + currentKey + "Value " + currentValue);

            characterInfoDictionary.Add(currentKey, currentValue);
        }
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
