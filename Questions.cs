using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class Questions : MonoBehaviour
{
    
    public List<TextMeshProUGUI> questions = new List<TextMeshProUGUI>();

    public List<GameObject> colliders = new List<GameObject>();

    public GameObject panel;

    public int currentindex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Questions");
            panel.SetActive(true);

            questions[currentindex].gameObject.SetActive(true);

            StartCoroutine(bekle());
        }
    }

    IEnumerator bekle(){
        yield return new WaitForSeconds(5);

        questions[currentindex].gameObject.SetActive(false);
        panel.SetActive(false);

        colliders[currentindex].SetActive(false);
        
        currentindex++;
    }
}