using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgMusicSource;

    public AudioSource bgMusicSource2;

    public AudioSource[] sfxSource;

    public AudioSource walkingSound;
    public AudioSource doorSound;
    public AudioSource dialogSound;
    public AudioSource woodenWalkSound;

    public AudioSource soruSound;

    public AudioSource gameOverSound;

    public AudioSource levelCompletedSound;

    public AudioSource monsterSound;

    



    private float bgMusicVolume = 1.0f;

    private float bgMusicVolume2 = 1.0f;
    private float sfxVolume = 1.0f;
    
    private float bgMusicTime = 0f;



    private void Awake(){
        if(Instance == null){
            Instance = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;


        }
        else{
            Destroy(gameObject);
            return;
        }

        LoadVolumeSettings();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        AssignAudioSources();

        if(bgMusicSource != null ){
            if(bgMusicSource.time != 0f){
                bgMusicSource.time = bgMusicTime;
                
            }            
            if(bgMusicSource.isPlaying == false){
                bgMusicSource.Play();
            }

        if(bgMusicSource2 != null){
            if(bgMusicSource2.time != 0f){
                bgMusicSource2.time = bgMusicTime;
                
            }   
            if(bgMusicSource2.isPlaying == false){
                bgMusicSource2.Play();
            }
        }
        }
        ApplyVolumeSettings();
    }


    private void AssignAudioSources()
    {
        bgMusicSource = GameObject.Find("bg")?.GetComponent<AudioSource>();

        bgMusicSource2 = GameObject.Find("bg2")?.GetComponent<AudioSource>();

        if(bgMusicSource != null)
        {
            bgMusicSource.loop = true;
        }
        if(bgMusicSource2 != null)
        {
            bgMusicSource2.loop = true;
        }

        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        sfxSource = System.Array.FindAll(allAudioSources, source => source != bgMusicSource && source != bgMusicSource2);

    }

    private void LoadVolumeSettings()
    {
        bgMusicVolume = PlayerPrefs.GetFloat("BGMusicVolume", 1f);
        bgMusicVolume2 = PlayerPrefs.GetFloat("BGMusicVolume2", 1f);

        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void ApplyVolumeSettings()
    {
        if(bgMusicSource != null)
        {
            bgMusicSource.volume = bgMusicVolume;
        }

        if(bgMusicSource2 != null)
        {
            bgMusicSource2.volume = bgMusicVolume2;
        }

        foreach(var sfx in sfxSource)
        {
            if(sfx != null)
            {
                sfx.volume = sfxVolume;
            }
        }
    }

    public void SetBGMusicVolume(float volume)
    {
        bgMusicVolume = volume;
        bgMusicVolume2 = volume;
        PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        PlayerPrefs.SetFloat("BGMusicVolume2", bgMusicVolume2);
        ApplyVolumeSettings();
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        ApplyVolumeSettings();
    }

    private void Update()
    {
        if(bgMusicSource != null && bgMusicSource.isPlaying)
        {
            bgMusicTime = bgMusicSource.time;
        }
        if(bgMusicSource2 != null && bgMusicSource2.isPlaying){
            bgMusicTime = bgMusicSource2.time;
            }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        PlayerPrefs.SetFloat("BGMusicVolume2", bgMusicVolume2);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    public void PlayAudio(AudioSource ses){
        if(!ses.isPlaying){
            ses.Play();
        }

    }

}
