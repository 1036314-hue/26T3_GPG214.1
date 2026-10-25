using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> gameObjects;
    public int destroyedBoxCount;

    private AudioClip clip;

    

    private string audioFileName = "Audio/YouDidTheThing.wav";

    private void OnCollisionStay()
    {
        for (int i = 0; i <= gameObjects.Count - 1; i++)
        {
            gameObjects[i].GetComponent<Rigidbody>().linearVelocity = speed * direction * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObjects.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        gameObjects.Remove(collision.gameObject);
        Destroy(collision.gameObject);
        StartCoroutine(LoadAudioClipFromFile());
        destroyedBoxCount++;
    }

    IEnumerator LoadAudioClipFromFile()
    {
        //Create path to file
        UnityWebRequest audioClipRequest = UnityWebRequestMultimedia.GetAudioClip(Path.Combine(Application.streamingAssetsPath, audioFileName), AudioType.WAV);
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
            //Debug.Log("Error with download at " + audioFileName);
            yield break;
        }

        //Debug.Log("Download Complete");

        clip = DownloadHandlerAudioClip.GetContent(audioClipRequest);

        AudioSource.PlayClipAtPoint(clip, Vector3.zero);

        audioClipRequest.Dispose();

        yield return null;
    }
}
