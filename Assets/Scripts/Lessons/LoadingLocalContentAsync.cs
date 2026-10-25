using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadingLocalContentAsync : MonoBehaviour
{
    public Texture texture;
    public string jsonData;
    public AudioClip clip;
    public AssetBundle bundle;
    public Sprite sprite;


    public string jsonFileName;
    public string imageFileName;
    public string audioFileName;
    public string assetBundleName;

    public string streamingAssetsFolderPath = Application.streamingAssetsPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return StartCoroutine(LoadLocalJsonAsync());
        //wait till complete to do next one

        yield return StartCoroutine(LoadTextureFromFile());

        yield return StartCoroutine(LoadAudioClipFromFile());

        yield return StartCoroutine(LoadAssetBundleFromFile());
    }


   

    IEnumerator LoadLocalJsonAsync()
    {
        UnityWebRequest jsonLoadingRequest = UnityWebRequest.Get(Path.Combine(streamingAssetsFolderPath, jsonFileName));

        AsyncOperation downloadOperation = jsonLoadingRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("Download progress " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }

        if (jsonLoadingRequest.result == UnityWebRequest.Result.ConnectionError || jsonLoadingRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error with download");
            yield break;
        }

        Debug.Log("Download Complete");

        //access web requestsx access the download handler grab text
        jsonData = jsonLoadingRequest.downloadHandler.text;
     
        jsonLoadingRequest.Dispose();
        
        yield return null;
    }

    IEnumerator LoadTextureFromFile()
    {
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(Path.Combine(streamingAssetsFolderPath, imageFileName));

        AsyncOperation downloadOperation = imageRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("Download progress " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }

        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error with download");
            yield break;
        }

        Debug.Log("Download Complete");

        byte[] allDataDownloaded = imageRequest.downloadHandler.data;

        Texture2D myTexture = DownloadHandlerTexture.GetContent(imageRequest);

        texture = myTexture;
        sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);

        imageRequest.Dispose();

        yield return null;
    }

    IEnumerator LoadAudioClipFromFile()
    {
        UnityWebRequest audioClipRequest = UnityWebRequestMultimedia.GetAudioClip(Path.Combine(streamingAssetsFolderPath, imageFileName),AudioType.WAV);

        AsyncOperation downloadOperation = audioClipRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("Download progress " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }

        if (audioClipRequest.result == UnityWebRequest.Result.ConnectionError || audioClipRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error with download");
            yield break;
        }

        Debug.Log("Download Complete");

        clip = DownloadHandlerAudioClip.GetContent(audioClipRequest);

        

        audioClipRequest.Dispose();

        yield return null;
    }

    IEnumerator LoadAssetBundleFromFile()
    {
        UnityWebRequest assetBundleRequest = UnityWebRequestAssetBundle.GetAssetBundle(Path.Combine(streamingAssetsFolderPath, assetBundleName));

        AsyncOperation downloadOperation = assetBundleRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("Download progress " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }

        if (assetBundleRequest.result == UnityWebRequest.Result.ConnectionError || assetBundleRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error with download");
            yield break;
        }

        Debug.Log("Download Complete");


        bundle = DownloadHandlerAssetBundle.GetContent(assetBundleRequest);

        assetBundleRequest.Dispose();

        yield return null;
    }

    private void ConvertDataTOJson()
    {

    }
}
