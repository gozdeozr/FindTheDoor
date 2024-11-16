using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionsSystem : MonoBehaviour
{

    public static QuestionsSystem Instance;
    public GameObject[] questionsTrigger;

    public GameObject[] dialogsTrigger;

    public GameObject levelFinishedTrigger;

    public GameObject panelDialog;
    public TextMeshProUGUI dialogText;

    public GameObject panelQuestions;
    public TextMeshProUGUI questionText;

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
        panelQuestions.SetActive(true);
        ItemSystem.Instance.Pot_panel.SetActive(false);
        
        StartCoroutine(bekle(15f, () => {
            
            panelQuestions.SetActive(false);
            ItemSystem.Instance.Pot_panel.SetActive(true);
               
        }));
    
    }

    public void Dialog(){
        panelDialog.SetActive(true);
        ItemSystem.Instance.Pot_panel.SetActive(false);

        StartCoroutine(bekle(5f, () => {
            
            panelDialog.SetActive(false);
            ItemSystem.Instance.Pot_panel.SetActive(true);
               
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
