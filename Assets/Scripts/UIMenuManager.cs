using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
	public class UIMenuManager : MonoBehaviour {

		public static UIMenuManager Instance;
		private Animator CameraObject;
        public GameObject mainMenu;
        public GameObject firstMenu;
        public GameObject playMenu;
        public GameObject exitMenu;
        public GameObject mainCanvas; 
		public GameObject levelCanvas;     
        public GameObject PanelGame;		
		public bool waitForInput = true;
        public GameObject loadingMenu;
        public Slider loadingBar;
        public TMP_Text loadPromptText;
		public KeyCode userPromptKey;
        public AudioSource hoverSound;       
        public AudioSource sliderSound;       
        public AudioSource swooshSound;
		
		

		void Awake(){
			if(Instance == null){
				Instance = this;
			}
			else{
				Destroy(gameObject);
			}	
		}


		void Start(){
			CameraObject = transform.GetComponent<Animator>();

		
			
			playMenu.SetActive(false);
			exitMenu.SetActive(false);
			firstMenu.SetActive(true);
			mainMenu.SetActive(true);

		}



		public void PlayCampaign(){
			exitMenu.SetActive(false);			
			playMenu.SetActive(true);
		}
		

		public void ReturnMenu(){
			playMenu.SetActive(false);			
			exitMenu.SetActive(false);
			mainMenu.SetActive(true);
		}


		public void Continue(){
		
			string sceneName = PlayerPrefs.GetString("sceneName");
			Time.timeScale = 1f;
			StartCoroutine(LoadAsynchronously(sceneName));
				
			
			
		}

		public void LoadScene(string scene){
			if(scene != ""){
				StartCoroutine(LoadAsynchronously(scene));
			}
		}

		public void  DisablePlayCampaign(){
			playMenu.SetActive(false);
		}

		public void Position2(){
			DisablePlayCampaign();
			CameraObject.SetFloat("Animate",1);
		}

		public void Position1(){
			CameraObject.SetFloat("Animate",0);
		}


		public void Position3(){
			
			CameraObject.SetBool("shop",true);
		}
		public void Position4(){
			CameraObject.SetBool("shop",false);
		}

		public void Position5(){
			CameraObject.SetBool("levels",true);
		}

		public void Position6(){
			CameraObject.SetBool("levels",false);
		}

		public void GamePanel(){
			
			PanelGame.SetActive(true);
			
		}

		public void PlayHover(){
			hoverSound.Play();
		}

		public void PlaySFXHover(){
			sliderSound.Play();
		}

		public void PlaySwoosh(){
			swooshSound.Play();
		}

		// Are You Sure - Quit Panel Pop Up
		public void AreYouSure(){
			exitMenu.SetActive(true);
			
			DisablePlayCampaign();
		}

		public void AreYouSureMobile(){
			exitMenu.SetActive(true);			
			mainMenu.SetActive(false);
			DisablePlayCampaign();
		}


		public void QuitGame(){
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}

		public void ShopBTN1(){			
			ShoppingSystem.Instance.FiyatGoster(1);
			
		}

		public void ShopBTN2(){
			ShoppingSystem.Instance.FiyatGoster(2);
		}

		public void buybtn1(){
			ShoppingSystem.Instance.BuyPotion1();
		}

		public void buybtn2(){
			ShoppingSystem.Instance.BuyPotion2();
		}

    
		public void NewGame()
    	{
        	float bgMusicVolume = PlayerPrefs.GetFloat("BGMusicVolume", 1f);
        	float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        	PlayerPrefs.DeleteAll();

        	PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        	PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        	PlayerPrefs.Save();

        	StartCoroutine(LoadAsynchronously("Level1"));
    	}
		IEnumerator LoadAsynchronously(string sceneName){ // scene name is just the name of the current scene being loaded
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;
			mainCanvas.SetActive(false);
			levelCanvas.SetActive(false);
			loadingMenu.SetActive(true);
			while(!operation.isDone){
				if(Input.touchCount > 0){
					foreach (Touch touch in Input.touches){
						if (touch.phase ==TouchPhase.Began ){						
							operation.allowSceneActivation = true;				
						}						
					}
				}
				yield return null;
			}
		}
}
