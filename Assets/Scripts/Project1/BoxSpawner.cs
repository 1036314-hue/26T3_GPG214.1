using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.U2D;

public class BoxSpawner : Singleton<BoxSpawner>
{
    [SerializeField]
    private string audioFileName = "Audio/BoxSpawned.wav";

    private string[] imageFileName = new string[6] { "Textures/Anny.png", "Textures/EllieMinibot.png", "Textures/Minikomew.png", "Textures/Neuro.png", "Textures/Shondo.png", "Textures/Toma.png" };

    public Texture2D texture;

    public AudioClip clip;

    private GameObject box;

    public string streamingAssetsFolderPath = Application.streamingAssetsPath;

    public float boxSpawnSpeed = 5;

    


    [SerializeField]
    private GameObject boxPrefab;

    private void Start()
    {
        StartCoroutine(SpawnBoxes());
    }

    IEnumerator SpawnBoxes()
    {
        while (true)
        {
            box = Instantiate(boxPrefab, new Vector3(-5, 6, -6.8f), Quaternion.identity);
            boxSpawnSpeed = boxSpawnSpeed / 1.1f;
            //Loading box texture from streaming assets folder
            yield return StartCoroutine(LoadTextureFromFile());
            //Playing the audio from streaming assets folder
            yield return StartCoroutine(LoadAudioClipFromFile());
            //Keeping Looping for indefinite spawning
            yield return new WaitForSeconds(boxSpawnSpeed);
        }
    }

    IEnumerator LoadTextureFromFile()
    {
        //Create path to file
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(Path.Combine(streamingAssetsFolderPath, imageFileName[Random.Range(1, 6)]));
        //Send path to request
        AsyncOperation downloadOperation = imageRequest.SendWebRequest();
        //Check download status
        while (!downloadOperation.isDone)
        {
            //Debug.Log("Download progress " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }
        //Error check 
        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            //Debug.Log("Error with download at " + imageFileName);
            yield break;
        }

        //Debug.Log("Download Complete");
        //convert to bytes
        byte[] allDataDownloaded = imageRequest.downloadHandler.data;
        //transform image request to texture
        Texture2D myTexture = DownloadHandlerTexture.GetContent(imageRequest);

        texture = myTexture;
        //apply texture
        box.GetComponent<Renderer>().material.mainTexture = texture;
        //dispose of request
        imageRequest.Dispose();

        yield return null;
    
}

    IEnumerator LoadAudioClipFromFile()
    {
        //Create path to file
        UnityWebRequest audioClipRequest = UnityWebRequestMultimedia.GetAudioClip(Path.Combine(streamingAssetsFolderPath, audioFileName), AudioType.WAV);
        //Give path to request
        AsyncOperation downloadOperation = audioClipRequest.SendWebRequest();
        //Checking if file done 
        while (!downloadOperation.isDone)
        {
            //Debug.Log("Download progress " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }

        if (audioClipRequest.result == UnityWebRequest.Result.ConnectionError || audioClipRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            //Debug.Log("Error with download");
            yield break;
        }

        //Debug.Log("Download Complete");

        clip = DownloadHandlerAudioClip.GetContent(audioClipRequest);

        AudioSource.PlayClipAtPoint(clip, Vector3.zero);

        audioClipRequest.Dispose();

        yield return null;
    }
}
