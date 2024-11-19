using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public static AnimationManager Instance;
    public Animator DoorAnimatorGiris;
    public Animator DoorAnimatorCikis;
    public Animator PlayerAnimator;

    public Animator FollowerAnimator;

    public Animator VirtualCamera;

    public Animator Soru;



    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }
    

    public void DoorGirisOpen()
    {
       DoorAnimatorGiris.SetBool("isOpen",true);
    }

    public void DoorCikisOpen()
    {
       DoorAnimatorCikis.SetBool("isOpen",true);
    }

    public void DoorGirisClose(){
        DoorAnimatorGiris.SetBool("isOpen",false);
    }

    public void DoorCikisClose()
    {
       DoorAnimatorCikis.SetBool("isOpen",false);
    }


    public void CharacterWalk(){
        PlayerAnimator.SetBool("isWalk",true);
        if(ItemSystem.Instance.isSpeed == true){
            PlayerAnimator.speed = 2f;
        }
        PlayerAnimator.speed = 1f;
    }

    public void CharacterStop(){
        PlayerAnimator.SetBool("isWalk",false);
    }

    public void CharacterRun(){
        VirtualCamera.SetBool("isRun",true);

    }

    public void CharacterStopRun(){
        VirtualCamera.SetBool("isRun",false);
    }

    //public void FollowerRun(){
        //FollowerAnimator.SetBool("isRun",true);
    //}
    
   
}
