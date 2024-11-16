using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuanSystem : MonoBehaviour
{
    public List<TextMeshPro> puanlar;

    public GameObject continueBTN;


    void Update(){
        Continue();
    }

    void Start(){
        for (int i = 0; i < puanlar.Count; i++)
        {
            puanlar[i].text = PlayerPrefs.GetInt("levelPuan" + i).ToString();
        }
    }

    public void Continue(){
        if(PlayerPrefs.GetInt("currentLevel")  != 0)
        {
            continueBTN.SetActive(true);
        }else{
            continueBTN.SetActive(false);
        }
        
    }


}
