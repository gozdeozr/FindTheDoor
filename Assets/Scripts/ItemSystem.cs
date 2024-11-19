using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{

    public static ItemSystem Instance;
    public GameObject[] pot_items;

    private int green_currentPotion;
    private int blue_currentPotion;

    public TextMeshProUGUI bluePotText;

    public TextMeshProUGUI greenPotText;

    private float speedUpTime;

    private bool isSpeedDown = false;

    private bool isSpeedUp = false;

    public bool isSpeed = false;

    public GameObject Pot_panel;

    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        green_currentPotion = PlayerPrefs.GetInt("green_currentPotion");
        blue_currentPotion = PlayerPrefs.GetInt("blue_currentPotion");
        speedUpTime = 10;
        greenPotText.text = green_currentPotion.ToString();
        bluePotText.text = blue_currentPotion.ToString();
    }

    void Update(){

        if (green_currentPotion == 0)
        {
            pot_items[0].SetActive(false);
        }
        if (blue_currentPotion == 0)
        {
            pot_items[1].SetActive(false);
        }  

        if(speedUpTime == 0&&!isSpeedDown){
            isSpeedDown = true;
            isSpeed = false;
            StopAllCoroutines();

            PlayerMovement.Instance.speed -= 2f;
            



    }
    }

    public void UsePot2(){

        if (blue_currentPotion > 0 && SayacSystem.Instance.isZero == false){
            
            blue_currentPotion -=1;
            bluePotText.text = blue_currentPotion.ToString();

            PlayerPrefs.SetInt("blue_currentPotion", blue_currentPotion);

            if(SayacSystem.Instance.saniye !=0){
                SayacSystem.Instance.saniye += 10;
            }
            

        }
    }
    public void UsePot1(){

        if (green_currentPotion > 0 && SayacSystem.Instance.isZero == false){
            
            isSpeedUp = false;

            speedUpTime = 10;
            
            green_currentPotion-=1;
            greenPotText.text = green_currentPotion.ToString();

            PlayerPrefs.SetInt("green_currentPotion", green_currentPotion);
            AudioManager.Instance.walkingSound.pitch = 0.63f;

            
            
            StartCoroutine(SpeedUpCoroutine());

            isSpeedDown = false;

            isSpeed = true;

        }

    }


    IEnumerator SpeedUpCoroutine(){

        while (speedUpTime > 0){

            if(!isSpeedUp){
                PlayerMovement.Instance.speed += 2f;
                isSpeedUp = true;

            }
            yield return new WaitForSeconds(1);
            

            speedUpTime -= 1;

            
            

        }
        AudioManager.Instance.walkingSound.pitch = 0.48f;




    }
    
}
