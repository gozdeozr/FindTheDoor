using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class Questions : MonoBehaviour
{

    public Dictionary<string, string> dialogs = new Dictionary<string, string>()
    {
        {"dialog1", "Birinci dialog"},
        {"dialog2", "İkinci dialog"},
        {"dialog3", "Üçüncü dialog"},
        {"dialog4", "Dördüncü dialog"},
        {"dialog5", "Beşinci dialog"}
    };

    public Dictionary<string, string> soru = new Dictionary<string, string>()
    {
        {"soru1", "Eminim Şuanda karşında gördüğün nesnenin ne olduğunu merak ediyorsundur , telaşlanma oyun yeni başlıyor hahaha ,etrafına dikkat etmen gerektiğini söylemiştim şimdi gördüğün gibi 2 yol ayrımı var ve bunlardan birini seçmelisin , neye göre seçicem dediğini duyar gibiyim işte soru şu gördüğün nesneden gelirken kaç adet gördün? "},
        {"soru2", "İkinci soru"},
        {"soru3", "Üçüncü soru"},
        {"soru4", "Dördüncü soru"},
        {"soru5", "Beşinci soru"}
    };

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DialogsTrigger")
        {

            QuestionsSystem.Instance.Dialog();
        }
        else if (other.gameObject.tag == "sahne"){

            AudioManager.Instance.PlayAudio(AudioManager.Instance.levelCompletedSound);
            if(AudioManager.Instance.bgMusicSource != null){
                AudioManager.Instance.bgMusicSource.Stop();
            }
            if(AudioManager.Instance.bgMusicSource2 != null){
                AudioManager.Instance.bgMusicSource2.Stop();
                
            }
            QuestionsSystem.Instance.LevelFinished();
            ItemSystem.Instance.Pot_panel.SetActive(false);
            SayacSystem.Instance.timePause = false;
            LevelManager.Instance.SahneGuncelle();

        }
        else if(other.gameObject.tag =="DoorGiris"){
            AudioManager.Instance.PlayAudio(AudioManager.Instance.doorSound);
            AnimationManager.Instance.DoorGirisOpen();
        }
        else if(other.gameObject.tag =="DoorCikis"){
            AudioManager.Instance.PlayAudio(AudioManager.Instance.doorSound);
            AnimationManager.Instance.DoorCikisOpen();
        }

        else if (dialogs.ContainsKey(other.gameObject.name))
        {
            QuestionsSystem.Instance.Dialog();
            AudioManager.Instance.PlayAudio(AudioManager.Instance.dialogSound);
            QuestionsSystem.Instance.dialogText.text = dialogs[other.gameObject.name];
            Destroy(other.gameObject);
        }
        else if (soru.ContainsKey(other.gameObject.name)){

            QuestionsSystem.Instance.Question();    
            AudioManager.Instance.PlayAudio(AudioManager.Instance.soruSound);
            QuestionsSystem.Instance.questionText.text = soru[other.gameObject.name];
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag =="wooden"){
            PlayerMovement.Instance.isAudioWooden = true;
            
            
            AudioManager.Instance.PlayAudio(AudioManager.Instance.woodenWalkSound);
    }

    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag =="DoorGiris"){
            AnimationManager.Instance.DoorGirisClose();
        }
        else if(other.gameObject.tag =="DoorCikis"){
            AnimationManager.Instance.DoorCikisClose();

        }
        else if(other.gameObject.tag =="wooden"){
            PlayerMovement.Instance.isAudioWooden = false;
            
            AudioManager.Instance.woodenWalkSound.Stop();
        }
    }
}