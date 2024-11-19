using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Questions : MonoBehaviour
{

    public Dictionary<string, string> dialogs = new Dictionary<string, string>()
    {
        {"dialog1", "Hey yabancı burada ne işin var senin?"},
        {"dialog2", "hmmm... kaybolmuş gibi bir halin var"},
        {"dialog3", "Sakin bir ormanda yürümek herkese iyi gelir, HA-HA-HA o kadar sakin olmayabilir"},
        {"dialog4", "Telaşlanma şaka yapıyorum sadece ;)"},
        {"dialog5", "Hadi biraz ormanı gezelim belki bir şeyler buluruz"},
        {"dialog6", "Zaman akıyor bunun ne olduğunu merak ettiğini biliyorum, yakında öğreneceksin :)"},
        {"dialog7", "Şimdi dikkatini toplamanın tam zamanı"},
        {"dialog8", "Gözüne çarpan bir şeyler oldu mu?"},
        {"dialog9", "Eminim Şuanda karşında gördüğün nesnenin ne olduğunu merak ediyorsundur"},
        {"dialog10", "Hmmm birinci bölümü geçebilmişsin ...Oyun şimdi başlıyor"},
        {"dialog11", "Bölümlerden kazandığın altınlarla marketten iksirler alabilirsin"},
        {"dialog12", "Bundan sonrası sandığın kadar kolay olmayabilir"},
        {"dialog13", "Bu yoldan gitmeni sana kim söyledi, işaretlere uysan iyi edersin"},
        {"dialog14", "Sanırım birileri burada kamp yapmış, ormanda yalnız değilsin yabancılara dikkat et"},
        {"dialog15", "Biraz yorulmuş görünüyorsun fakat kapıyı bulsan iyi edersin"},
        {"dialog16", "Kapıyı buldun koş koş koş koş..."},
        {"dialog17", "Kapıyı kapattın diye Spitter'dan kaçtığını düşünmeye kalkma sadece gelmesi biraz zaman alıcak"},
        {"dialog18", "Artık oyunu anlamış olmalısın , doğru yolu bul ve Spitter gelmeden kapıya ulaş !"},
        {"dialog19", "Upsss... yanlış yol "},
        {"dialog20", "Bir sonraki bölümde neler yapıcaksın merak ediyorum, iyi bir oyuncusun"},
      
    };

    public Dictionary<string, string> soru = new Dictionary<string, string>()
    {
        {"soru1", "telaşlanma oyun yeni başlıyor hahaha, etrafına dikkat etmen gerektiğini söylemiştim şimdi gördüğün gibi 2 yol ayrımı var ve bunlardan birini seçmelisin, neye göre seçicem dediğini duyar gibiyim işte soru şu gördüğün nesneden gelirken kaç adet gördün? "},
        {"soru2", "Evet yeni soru, ilk bölümdeki sayac süresi ne kadardı?"},
        {"soru3", "Odada bir işaret görmüş olman lazım, gördüğün işaret bunlardan hangisiydi?"},
        {"soru4", "Gelirken duvarlardaki çiçeklere dikkat ettin mi? Kırmızı çiçekler kaç taneydi?"},
        {"soru5", "Kardan adamın üzerinde kaç adet düğme gördün?"},
        {"soru6", "Arkadaki dağda dikkatini çeken bir şey oldu mu?"},
        {"soru7", "Sarı ağaçların sayısı ne kadardı?"},
        {"soru8", "Bir önceki soruya doğru cevabı verdiğini düşünüyor musun?"},
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
            if(SceneManager.GetActiveScene().buildIndex != 1){
                AudioManager.Instance.monsterWalkSound.Stop();
                Follower.Instance.isCatched = true;

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
            QuestionsSystem.Instance.StopAllCoroutines();
            QuestionsSystem.Instance.Dialog();
            AudioManager.Instance.PlayAudio(AudioManager.Instance.dialogSound);
            QuestionsSystem.Instance.questionDialogText.text = dialogs[other.gameObject.name];
            Destroy(other.gameObject);
        }
        else if (soru.ContainsKey(other.gameObject.name)){
            QuestionsSystem.Instance.StopAllCoroutines();
            QuestionsSystem.Instance.Question();    
            AudioManager.Instance.PlayAudio(AudioManager.Instance.soruSound);
            QuestionsSystem.Instance.questionDialogText.text = soru[other.gameObject.name];
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