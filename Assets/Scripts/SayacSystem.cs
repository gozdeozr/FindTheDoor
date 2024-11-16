using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class SayacSystem : MonoBehaviour


{

    public static SayacSystem Instance;
    public TextMeshProUGUI sayimText;

    public TextMeshProUGUI puanText;
    public TextMeshProUGUI coinText;
    public int dakika ;
    public int saniye ;

    public bool isZero = false;

    public bool timePause;

    public int puan;

    public int coin;

    private int toplamCoin;



    

    
    private void Awake(){

        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sayimText.text = dakika.ToString("D2") + ":" + saniye.ToString("D2");
        StartCoroutine(GeriSayimCoroutine());
        timePause = true;
        toplamCoin = PlayerPrefs.GetInt("toplamCoin");
    }

    public void Update(){
        SayacSifirlandi();

        if(timePause == false){   
            StopAllCoroutines();        
            PuanHesapla();
            CoinHesapla();
            
        }

        if(saniye>59){
            dakika++;
            saniye -=60;

        }

    }

    IEnumerator GeriSayimCoroutine()
    {
            while (dakika > 0 || saniye > 0)
            {
                yield return new WaitForSeconds(1);
                saniye--;
                if (saniye < 0)
                {
                    dakika--;
                    saniye =59;
                }
                sayimText.text = dakika.ToString("D2") + ":" + saniye.ToString("D2");
            }
        
    }

    public void SayacSifirlandi(){
        if(SceneManager.GetActiveScene().buildIndex != 1){
            if (dakika == 0 && saniye == 0){
                    isZero = true;
                    PlayerMovement.Instance.isSpeedUp = true;
                            
            }
        }

    }

    public void PuanHesapla(){      
        puan = ((dakika*60) + saniye)*10;
        PlayerPrefs.SetInt("puan", (puan+=100));
        puanText.text = "Your Score:"+puan.ToString();
        

        if(puan>PlayerPrefs.GetInt("levelPuan"+(PlayerPrefs.GetInt("currentLevel")-1))){
            int currentLevel = PlayerPrefs.GetInt("currentLevel");

            PlayerPrefs.SetInt("levelPuan" + (currentLevel-1), puan);
        }
    }       

    public void CoinHesapla(){

        coin = puan / 10;
        coinText.text = "Coins:"+coin.ToString();
        
        
        PlayerPrefs.SetInt("toplamCoin",toplamCoin+=coin);
        timePause = true;

    }
}
