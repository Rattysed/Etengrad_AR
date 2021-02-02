using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;

    public Sprite s1, s2;
    public Button musBut;

    public void musicButton()
    {
        audioSource.mute = !audioSource.mute;
        if (audioSource.mute)
        {
            musBut.GetComponent<Image>().sprite = s2;
        }
        else
        {
            musBut.GetComponent<Image>().sprite = s1;
        }
    }
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
    }

    private AudioClip GetRandomClip()

    {
        return clips[Random.Range(0, clips.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }
}
