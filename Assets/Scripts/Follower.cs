using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private bool isFollowing = false;

    public GameObject sayac;

    public GameObject cinemation;

    public GameObject GameOver;

    public GameObject cameraFollow;

    public GameObject playerObject;

    bool isCamera = false;
    public GameObject backGroundSes;
    public GameObject backGroundSes2;

    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  
        backGroundSes2.SetActive(false);     
    }

    void Update()
    {   
        
        Followed();
    }

    public void Followed()
    {
        if (sayac.GetComponent<SayacSystem>().isZero == true)
        {   
            if(!isCamera)
            {
                isCamera = true;
                AudioManager.Instance.walkingSound.Stop();
                
                cameraAnim();
                backGroundSes.SetActive(false);
                AudioManager.Instance.PlayAudio(AudioManager.Instance.monsterSound);
                StartCoroutine(CameraAnimation(12f));      
            }
            if (isFollowing)
            {
            agent.SetDestination(player.position);
            }            
        }
        else{
            isFollowing = false;
            cinemation.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider collision){
        if (collision.gameObject.tag == "Player"){
            AudioManager.Instance.PlayAudio(AudioManager.Instance.gameOverSound);
            GameOver.SetActive(true);
            AudioManager.Instance.bgMusicSource.Stop();
            AudioManager.Instance.bgMusicSource2.Stop();
            Time.timeScale = 0f;
        }
    }

    public void cameraAnim(){
        if(isCamera == true){
            playerObject.SetActive(false); 
            cameraFollow.SetActive(true);
            

        }
    }


    IEnumerator CameraAnimation(float sure){

       yield return new WaitForSeconds(sure);
       playerObject.SetActive(true);
       cameraFollow.SetActive(false);
       cinemation.SetActive(true);
       AudioManager.Instance.monsterSound.Stop();
       backGroundSes2.SetActive(true);
       isFollowing = true;

       
        

    
}   

    

    

    






}