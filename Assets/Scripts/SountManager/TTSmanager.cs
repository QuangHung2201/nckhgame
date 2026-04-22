using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class TTSManager : MonoBehaviour
{
    public static TTSManager instance;
    public AudioSource audioSource;

    private Queue<string> queue = new Queue<string>();
    private bool isPlaying = false;

    private void Start()
    {
        instance = this;
    }
    public void Speak(string text)
    {
        queue.Enqueue(text);

        if (!isPlaying)
            StartCoroutine(ProcessQueue());
    }

    IEnumerator ProcessQueue()
    {
        isPlaying = true;

        while (queue.Count > 0)
        {
            string text = queue.Dequeue();
            yield return StartCoroutine(PlayTTS(text));
        }

        isPlaying = false;
    }

    IEnumerator PlayTTS(string text)
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&q="
                     + UnityWebRequest.EscapeURL(text)
                     + "&tl=vi&client=tw-ob";

        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = clip;
            audioSource.Play();

            yield return new WaitForSeconds(clip.length);
        }
        else
        {
            Debug.LogError("TTS lỗi: " + www.error);
        }
    }
}

