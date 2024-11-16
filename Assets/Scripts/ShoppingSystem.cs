using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingSystem : MonoBehaviour
{
    public static ShoppingSystem Instance;
    public TextMeshPro coin;
    public TextMeshPro priceText;
    public int green_potion_price;
    public int blue_potion_price;
    public int green_currentPotion;
    public int blue_currentPotion;
    public GameObject buybtn;
    public GameObject buybtn2;

    public TextMeshPro green_current_equipment;
    public TextMeshPro blue_current_equipment;

    private int toplamCoin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        coin.text = "Altın:"+PlayerPrefs.GetInt("toplamCoin").ToString();
        green_current_equipment.text = PlayerPrefs.GetInt("green_currentPotion").ToString();
        blue_current_equipment.text = PlayerPrefs.GetInt("blue_currentPotion").ToString();
    }

    

    public void BuyPotion1(){
        

            PlayerPrefs.SetInt("toplamCoin", PlayerPrefs.GetInt("toplamCoin") - green_potion_price);

            green_currentPotion+=1;

            PlayerPrefs.SetInt("green_currentPotion", green_currentPotion);

            coin.text = "Altın:"+PlayerPrefs.GetInt("toplamCoin").ToString();

            green_current_equipment.text = PlayerPrefs.GetInt("green_currentPotion").ToString();

            if (PlayerPrefs.GetInt("toplamCoin") < green_potion_price){
                buybtn.SetActive(false);
            }
    }

    public void BuyPotion2(){
        

            PlayerPrefs.SetInt("toplamCoin", PlayerPrefs.GetInt("toplamCoin") - blue_potion_price);

            blue_currentPotion +=1;

            PlayerPrefs.SetInt("blue_currentPotion", blue_currentPotion);

            coin.text = "Altın:"+PlayerPrefs.GetInt("toplamCoin").ToString();

            blue_current_equipment.text = PlayerPrefs.GetInt("blue_currentPotion").ToString();

            if (PlayerPrefs.GetInt("toplamCoin") < blue_potion_price){
                buybtn2.SetActive(false);
            }
        


    }

    public void FiyatGoster(int potionNumber)
    {
        if (potionNumber == 1)
        {
            if(PlayerPrefs.GetInt("toplamCoin") >= green_potion_price){
                buybtn2.SetActive(false);
                buybtn.SetActive(true);
            }else{
                buybtn.SetActive(false);
                buybtn2.SetActive(false);
            }
            priceText.text = green_potion_price.ToString() + "$";
        }
        else
        {
            if(PlayerPrefs.GetInt("toplamCoin") >= blue_potion_price){
                buybtn.SetActive(false);
                buybtn2.SetActive(true);
            }else{
                buybtn2.SetActive(false);
                buybtn.SetActive(false);
            }
            priceText.text = blue_potion_price.ToString() + "$";
        }
    }
}