using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour

{

    public GameObject settingsMenu;

    public GameObject pauseMenu;


    public void Resume(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        if(SceneManager.GetActiveScene().buildIndex != 1){
            if(SayacSystem.Instance.isZero == true){
                if(AudioManager.Instance.bgMusicSource2 != null){

                AudioManager.Instance.PlayAudio(AudioManager.Instance.bgMusicSource2);
                
            }   
            }
            
            ItemSystem.Instance.Pot_panel.SetActive(true);
            
        }
        if(AudioManager.Instance.bgMusicSource != null)
            AudioManager.Instance.PlayAudio(AudioManager.Instance.bgMusicSource);

    }
    public void Pause(){
        if(AudioManager.Instance.bgMusicSource.isPlaying || AudioManager.Instance.bgMusicSource2.isPlaying){
            AudioManager.Instance.bgMusicSource.Pause();
            AudioManager.Instance.bgMusicSource2.Pause();
        }

        AudioManager.Instance.monsterWalkSound.Pause();
        ItemSystem.Instance.Pot_panel.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");


    }

    public void Settings(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart(){   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void bluepotBTN(){

        ItemSystem.Instance.UsePot2();



    }
    public void greenpotBTN(){
        if(ItemSystem.Instance.isSpeed == false){

            ItemSystem.Instance.UsePot1();
            
        }
    
}
}