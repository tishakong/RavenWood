using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip ADGetItem;
    public AudioClip ADBoiling;
    public AudioClip ADUIBtnClk;
    public AudioClip ADDrinking;
    public AudioClip ADMatch;
    public AudioClip ADPouring;
    public AudioClip ADDrawer;
    public AudioClip ADDoor;
    public AudioClip ADKey;
    public AudioClip ADCloset;
    public AudioClip ADDead;

    public AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        GameObject go = new GameObject("AD" + action);
        audioSource = go.AddComponent<AudioSource>();
        DontDestroyOnLoad(go);

        switch (action)
        {
            case "GetItem":
                audioSource.clip = ADGetItem; break;
            case "Boiling":
                audioSource.clip = ADBoiling; break;
            case "UIBtnClk":
                audioSource.clip = ADUIBtnClk; break;
            case "Drinking":
                audioSource.clip = ADDrinking; break;
            case "Match":
                audioSource.clip = ADMatch; break;
            case "Pouring":
                audioSource.clip = ADPouring; break;
            case "Drawer":
                audioSource.clip = ADDrawer; break;
            case "Door":
                audioSource.clip = ADDoor; break;
            case "Key":
                audioSource.clip = ADKey; break;
            case "Closet":
                audioSource.clip = ADCloset; break;
            case "Dead":
                audioSource.clip = ADDead; break;
        }
        audioSource.Play();
        Destroy(go, audioSource.clip.length);
    }
}
