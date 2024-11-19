using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    public string[] levelAdi;

    private int currentLevel = 0;


    public GameObject levelCanvas;
    public GameObject loadingMenu;

    public GameObject mainCanvas;



    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }
        
    

    void Start(){

        currentLevel = PlayerPrefs.GetInt("currentLevel");

        
    }





    
    public void SahneGuncelle(){
            
        currentLevel = SceneManager.GetActiveScene().buildIndex; 

        PlayerPrefs.SetInt("currentLevel", currentLevel);

        string sceneName = levelAdi[currentLevel];

        PlayerPrefs.SetString("sceneName", sceneName);

    }

    public void SahneCagir(){
        SahneGuncelle();
        Time.timeScale = 1f;
        StartCoroutine(LoadAsynchronously1(PlayerPrefs.GetString("sceneName")));
        Lightmapping.Bake();


    }

    public void SahneRestart(){
        SahneGuncelle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Lightmapping.Bake();
        Time.timeScale = 1f;
    }

    IEnumerator LoadAsynchronously1(string sceneName){ 
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;
            mainCanvas.SetActive(false);
			levelCanvas.SetActive(false);
			loadingMenu.SetActive(true);
			while(!operation.isDone){
				if(Input.touchCount > 0){
					foreach (Touch touch in Input.touches){
						if (touch.phase ==TouchPhase.Began ){						
							operation.allowSceneActivation = true;				
						}						
					}
				}
				yield return null;
			}
		}


   


    
        
    
    



}
