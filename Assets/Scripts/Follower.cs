using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Follower : MonoBehaviour
{

    public static Follower Instance;
    public Transform player;
    private NavMeshAgent agent;
    public bool isFollowing = false;

    public GameObject sayac;

    public GameObject cinemation;

    public GameObject GameOver;

    public GameObject cameraFollow;

    public GameObject playerObject;

    bool isCamera = false;
    public GameObject backGroundSes;
    public GameObject backGroundSes2;

    public Animator animator;

    public GameObject monsterDialog;

    public bool isCatched = false;


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
        agent = GetComponent<NavMeshAgent>();      
        animator = GetComponent<Animator>();
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
                if(SceneManager.GetActiveScene().buildIndex == 2){
                    cameraAnim();
                    AudioManager.Instance.bgMusicSource.Stop();
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.monsterSound);
                    StartCoroutine(CameraAnimation(12f));
                }
                else{
                    cinemation.SetActive(true);
                    AudioManager.Instance.bgMusicSource.Stop();
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.bgMusicSource2);
                    isFollowing = true;
                }    
            }
            if (isFollowing)
            {
                animator.SetBool("isWalk",true);
                if(isCatched == false){
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.monsterWalkSound);
                }
                
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
            isCatched = true;         
            AudioManager.Instance.bgMusicSource2.Stop();
            AudioManager.Instance.monsterWalkSound.Stop();
            Time.timeScale = 0f;
            ItemSystem.Instance.Pot_panel.SetActive(false);
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
       monsterDialog.SetActive(true);
       StartCoroutine(CameraAnimation2(5f));
       

       
        

    
}
    IEnumerator CameraAnimation2(float sure){

       yield return new WaitForSeconds(sure);
       playerObject.SetActive(true);
       monsterDialog.SetActive(false);
       cameraFollow.SetActive(false);
       cinemation.SetActive(true);
       AudioManager.Instance.monsterSound.Stop();
       AudioManager.Instance.PlayAudio(AudioManager.Instance.bgMusicSource2);
       isFollowing = true;
       

    }

    

    

    






}