using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionsSystem : MonoBehaviour
{

    public static QuestionsSystem Instance;
  
    public GameObject levelFinishedTrigger;


    public GameObject panelQuestionsDialog;
    public TextMeshProUGUI questionDialogText;

    public GameObject panelLevelFinished;

    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    public void Question(){
        panelQuestionsDialog.SetActive(true);
        ItemSystem.Instance.Pot_panel.SetActive(false);
        
        StartCoroutine(bekle(10f, () => {
            
            panelQuestionsDialog.SetActive(false);

            if(SceneManager.GetActiveScene().buildIndex !=1 ){
                ItemSystem.Instance.Pot_panel.SetActive(true);
            }
               
        }));
    
    }

    public void Dialog(){
        panelQuestionsDialog.SetActive(true);
        ItemSystem.Instance.Pot_panel.SetActive(false);

        StartCoroutine(bekle(5f, () => {
            
            panelQuestionsDialog.SetActive(false);
            if(SceneManager.GetActiveScene().buildIndex !=1 ){
                ItemSystem.Instance.Pot_panel.SetActive(true);
            }
               
        }));
    
    }

    public void LevelFinished(){
        panelLevelFinished.SetActive(true);
        Time.timeScale = 0f;
    }


    public IEnumerator bekle(float seconds, System.Action callback){
        yield return new WaitForSeconds(seconds);
        callback();
}

    
}
