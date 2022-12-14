using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region GameManager
    public static GameManager _GAME_MANAGER;
    #endregion    
    #region Turbo
    public float turboPower;
    public float turboPowerReamining;
    public bool theresTurboRemaining;
    public bool isActive;
    #endregion
    #region Mission
    public int missionIndex = -1;
    public int currentMission;
    public bool missionCompleted;
    public float currentReward;
    [System.Serializable]
    public struct Missions
    {
        public string missionTitle;
        public string descriptionTitle;
        public GameObject missionBrienfing;
        public GameObject departPoint;
        public GameObject arrivingPoint;
        public bool isTimeTrial;
        public float timeToBeat;
        public float waitTime;
        public float reward;
        public GameObject[] ruteSignsEasy;
        public GameObject[] ruteSignsMedium;
        public GameObject[] ruteSignsHard;
        public GameObject[] ruteSignsTimeTrial;
        public bool isCompleted;
    }
    [NonReorderable]
    public List<Missions> mission = new List<Missions>();
    public Text missionTitle;
    public Text missionDescription;
    public Text missionReward;
    public Text countDown;
    public Text timeToBeat;
    public GameObject missionBriefing;
    public GameObject _countdown;
    public GameObject _timeTrial;
    public GameObject _ruteButtons;
    public GameObject _startTrialButton;
    public float _time_;
    public float _timeToBeat;
    public bool isWaiting;
    public bool startsTrial;
    bool ruteSelected;
    #endregion
    #region Player
    public float currentPlayerMoney;
    public GameObject player;
    public Transform resetPoint;
    public Text turboLeft;
    public Image turboVar;
    #region PlayerUI
    public Text playerMoney;
    #endregion
    #endregion
    #region Menu&SplashScreen
    public Scene m_Scene;
    public string sceneName;
    public GameObject Menu;
    public GameObject Options;
    public GameObject Credits;
    public GameObject menuInGame;
    public GameObject optionsInGame;
    public GameObject menuButtons;
    #endregion
    #region Shop
    public GameObject shopObject;
    public PlayerController playerController;
    public bool isOpen;
    public bool turboUpgraded;
    public bool velUpgraded;
    public float velUpgrade_;
    #endregion
    private void Awake()
    {
        if(_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(_GAME_MANAGER);
        }
        else
        {
           _GAME_MANAGER = this;
            DontDestroyOnLoad(this);
        }
        
    }
    void Start()
    {
        if(playerController != null)
        {
            velUpgrade_ = playerController.maxVelocity;
        }
       
        if (_startTrialButton != null)
        {
            _startTrialButton.SetActive(false);
        }
        if (menuInGame != null && menuButtons != null && optionsInGame != null)
        {
            menuInGame.SetActive(false);
            optionsInGame.SetActive(false);
            menuButtons.SetActive(false);
        }
        if(Menu != null && Options != null && Credits != null)
        {
            Menu.SetActive(true);
            Options.SetActive(false);
            Credits.SetActive(false);
        }
        if(_countdown != null)
        {
            _countdown.SetActive(false);
        }
        if(_timeTrial != null)
        {
            _timeTrial.SetActive(false);
        }
        if(shopObject != null)
        {
            shopObject.SetActive(false);
        }
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        turboPowerReamining = turboPower;
        if(missionBriefing != null)
        {
            missionBriefing.SetActive(false);
        }
        for(int i =0; i < mission.Capacity; i++)
        {
            if (mission[i].isTimeTrial)
            {
                var ruteTT = mission[i];
                ruteTT.ruteSignsTimeTrial = GameObject.FindGameObjectsWithTag("TimeTrialSigns");
                mission[i] = ruteTT;
            }
            for(int j =0; j < mission[i].ruteSignsEasy.Length; j++)
            {
                if(mission[i].ruteSignsEasy[j] != null)
                {
                    mission[i].ruteSignsEasy[j].SetActive(false);
                }
                
            }
            for (int k = 0; k < mission[i].ruteSignsMedium.Length; k++)
            {
                if(mission[i].ruteSignsMedium[k] != null)
                {
                    mission[i].ruteSignsMedium[k].SetActive(false);
                }
                
            }
            for (int l = 0; l < mission[i].ruteSignsHard.Length; l++)
            {
                if (mission[i].ruteSignsHard[l] != null)
                {
                    mission[i].ruteSignsHard[l].SetActive(false);
                }
                
            }
            for (int l = 0; l < mission[i].ruteSignsTimeTrial.Length; l++)
            {
                if (mission[i].ruteSignsTimeTrial[l] != null)
                {
                    mission[i].ruteSignsTimeTrial[l].SetActive(false);
                }

            }
        }
        

    }
    void Update()
    {
        if (turboLeft != null)
        {
            turboLeft.text = turboPowerReamining.ToString();

        }
        if (turboVar != null)
        {
            turboVar.fillAmount = turboPowerReamining / 100;
        }
        
        if (turboUpgraded)
        {
            turboPowerReamining = turboPower;
            turboUpgraded = false;
        }
        if(sceneName == "SplashScreen")
        {
            
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("MenuPrincipal");
            }
        }
        if (missionIndex < 0 )
        {
            missionIndex++;
        }
       
        currentMission = missionIndex;
        if (turboPowerReamining >= 1)
        {
            theresTurboRemaining = true;
        }
        if (turboPowerReamining >= turboPower)
        {
            turboPowerReamining = turboPower;
        }
        if(playerMoney != null)
        {
            playerMoney.text = currentPlayerMoney.ToString();
        }    
        if (!ruteSelected && missionTitle !=null)
        {
            mission[currentMission].departPoint.SetActive(false);
            for (int i = 0; i < mission[currentMission].ruteSignsEasy.Length; i++)
            {
                if(mission[currentMission].ruteSignsEasy[i] != null)
                {
                    mission[currentMission].ruteSignsEasy[i].SetActive(false);
                }
                

            }
            for (int i = 0; i < mission[currentMission].ruteSignsMedium.Length; i++)
            {
                if(mission[currentMission].ruteSignsMedium[i] != null)
                {
                    mission[currentMission].ruteSignsMedium[i].SetActive(false);
                }
               

            }
            for (int i = 0; i < mission[currentMission].ruteSignsHard.Length; i++)
            {
                if (mission[currentMission].ruteSignsHard[i] != null)
                {
                    mission[currentMission].ruteSignsHard[i].SetActive(false);
                }
                

            }
        }
        if(sceneName == "SplashScreen" && sceneName == "MenuPrincipal" && sceneName == "TheEnd")
        {
            if (mission[currentMission].isTimeTrial)
            {
                if (_ruteButtons != null && _startTrialButton != null)
                {
                    _ruteButtons.SetActive(false);
                    _startTrialButton.SetActive(true);
                }

            }
            if (mission[currentMission].isTimeTrial == false)
            {
                if (_ruteButtons != null && _startTrialButton != null)
                {
                    _ruteButtons.SetActive(true);
                    _startTrialButton.SetActive(false);
                }
            }
        }
            
        
       
        
        if (ruteSelected && !isWaiting)
        {
            if (mission[currentMission].departPoint != null)
            {
                mission[currentMission].departPoint.SetActive(true);
            }
            
        }

        if (startsTrial)
        {
            DecreaseWaitTime();
            if (mission[currentMission].isTimeTrial == true && _time_ <= 0 && mission[currentMission].isCompleted == false)
            {
                SceneManager.LoadScene("End");
            }
            else if(mission[currentMission].isTimeTrial == true && _time_ <= 0 && mission[currentMission].isCompleted == false)
            {
                Debug.Log("sdaads");
                //SceneManager.LoadScene("End");
            }
        }
        
        if (isWaiting)
        {
            
            ScapeWaiting();
        }  
    }
    #region TurboSystem
    public void TurboOn()
    {   
        if (theresTurboRemaining && turboPowerReamining >=0)
        {
            turboPowerReamining -= Time.deltaTime * 10;
        }
        if (turboPowerReamining <= 0)
        {
            theresTurboRemaining = false;
            
        }
        if (!theresTurboRemaining)
        {
            turboPowerReamining = 0;
            
        } 
    }       
    public void TurboOff()
    {
        if(turboPowerReamining < 100)
        {
            turboPowerReamining+=Time.deltaTime;
        }   
    }
    public IEnumerator RecoverTurbo()
    {
        yield return new WaitForSeconds(1f);
        TurboOff();
    }
    #endregion
    #region Rutes
    public void Rute1()
    {        _time_ = mission[currentMission].waitTime;
        mission[currentMission].arrivingPoint.SetActive(false);
        ruteSelected = true;
        _time_ = mission[currentMission].waitTime;
        for (int i = 0; i <mission[currentMission].ruteSignsEasy.Length; i++)
        {
            mission[currentMission].ruteSignsEasy[i].SetActive(true);

        }
        Time.timeScale = 1;
        if(missionBriefing != null)
        {
            missionBriefing.SetActive(false);
        }     
        mission[currentMission].missionBrienfing.SetActive(false);
    }
    public void Rute2()
    {
        _time_ = mission[currentMission].waitTime;
        mission[currentMission].arrivingPoint.SetActive(false);
        ruteSelected = true;
        _time_ = mission[currentMission].waitTime;
        for (int i = 0; i < mission[currentMission].ruteSignsMedium.Length; i++)
        {
            mission[currentMission].ruteSignsMedium[i].SetActive(true);

        }
        Time.timeScale = 1;
        missionBriefing.SetActive(false);
        mission[currentMission].missionBrienfing.SetActive(false);
        var onGoingMission = mission[currentMission];
        currentReward = onGoingMission.reward;
        currentReward *= 2;
        onGoingMission.reward = currentReward;
        mission[currentMission] = onGoingMission;
    }
    public void Rute3()
    {
        _time_ = mission[currentMission].waitTime;
        mission[currentMission].arrivingPoint.SetActive(false);
        ruteSelected = true;
        _time_ = mission[currentMission].waitTime;
        for (int i = 0; i < mission[currentMission].ruteSignsHard.Length; i++)
        {
            mission[currentMission].ruteSignsHard[i].SetActive(true);
           

        }
        Time.timeScale = 1;
        missionBriefing.SetActive(false);
        mission[currentMission].missionBrienfing.SetActive(false);
        var onGoingMission = mission[currentMission];
        currentReward = onGoingMission.reward;
        currentReward *= 3;
        onGoingMission.reward = currentReward;
        mission[currentMission] = onGoingMission;

    }
    public void TimeTrialRute()
    {
        
        startsTrial = true;
        _timeToBeat = mission[currentMission].timeToBeat;
        _time_ = _timeToBeat;
        ruteSelected = true;
        mission[currentMission].arrivingPoint.SetActive(true);
        for (int i = 0; i < mission[currentMission].ruteSignsTimeTrial.Length; i++)
        {
            mission[currentMission].ruteSignsTimeTrial[i].SetActive(true);


        }
        _timeTrial.SetActive(false);
        missionBriefing.SetActive(false);
        Time.timeScale = 1;
        mission[currentMission].missionBrienfing.SetActive(false);
        var onGoingMission = mission[currentMission];
        currentReward = onGoingMission.reward;
        currentReward *= 10;
        onGoingMission.reward = currentReward;
        mission[currentMission] = onGoingMission;
    }
    #endregion
    #region ButtonsFunctions
    public void CreditsOn()
    {
        Menu.SetActive(false);
        Credits.SetActive(true);
    }
    public void OptionsOn()
    {
        Menu.SetActive(false);
        Options.SetActive(true);
    }
    public void BackToMenu()
    {
        Menu.SetActive(true);
        Options.SetActive(false);
        Credits.SetActive(false);
    }
    public void ExitGame()
   {
        Application.Quit();
   }
    public void LoadGame()
    {
        SceneManager.LoadScene("Vertical");
    }
    public void OpenInGameMenu()
    {
        menuInGame.SetActive(true);
        menuButtons.SetActive(true);
        Time.timeScale = 0;
    }
    public void OpenOptionsInGame()
    {
        menuButtons.SetActive(false);
        optionsInGame.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseOptionsInGame()
    {
        menuButtons.SetActive(true);
        optionsInGame.SetActive(false);
        Time.timeScale = 0;
    }
    public void CloseInGameMenu()
    {
        menuInGame.SetActive(false);
        optionsInGame.SetActive(false);
        menuButtons.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
        Time.timeScale = 1;
    }
    public void OpenShop()
    {
        isOpen = true;
        
        shopObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseShop()
    {
        isOpen = false;
        shopObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void UpgradeTurbo()
    {




       
        if (currentPlayerMoney >= 200 )
        {
            
            turboPower *= 2;
            currentPlayerMoney -= 200;
            turboUpgraded = true;
            
           
            
        }
        if (turboPower == 1600)
        {
            turboPower = 800;

        }





    }
    public void UpgradeVel()
    {





        if (currentPlayerMoney >= 300)
        {

            velUpgrade_ *= 2;
            currentPlayerMoney -= 300;
            velUpgraded = true;



        }
        if (playerController.maxVelocity == 1600)
        {
            playerController.maxVelocity = 800;

        }





    }
    #endregion
    #region MissionFunctions
    public void InitMission()
    {
        if(missionTitle != null)
        {
            missionTitle.text = mission[currentMission].missionTitle;
        }
       if(missionDescription != null)
       {
            missionDescription.text = mission[currentMission].descriptionTitle;
       } 
       if(missionReward != null)
       {
            missionReward.text = mission[currentMission].reward.ToString();
       }
       if (mission[currentMission].isTimeTrial)
       {
            _timeTrial.SetActive(true);
            timeToBeat.text = mission[currentMission].timeToBeat.ToString();
       }
        missionBriefing.SetActive(true);

    }
    public void ScapeWaiting()
    {


        _countdown.SetActive(true);
        
        if (isWaiting)
        {

            _time_ -= Time.deltaTime * 10;
            countDown.text = _time_.ToString();
        }
       
        if(_time_ <= 0)
        {
            /*isWaiting = false;
            mission[currentMission].arrivingPoint.SetActive(true);*/
            //
            _time_ = 0;
     
            mission[currentMission].arrivingPoint.SetActive(true);
            if(_countdown != null)
            {
                _countdown.SetActive(false);
            }
            isWaiting = false;
            
            


        }
    }
    public void DecreaseWaitTime()
    {
       
        _countdown.SetActive(true);
        if(_time_ > 0)
        {
            _time_ -= Time.deltaTime * 10;
        }
        if (_time_ <= 0)
        {
            _time_ = 0;
        }
        countDown.text = _time_.ToString();
    }
    public void EndMission()
    {
        startsTrial = false;
        var onGoingMission = mission[currentMission];
        missionCompleted = true;
        onGoingMission.isCompleted = missionCompleted;
        mission[currentMission] = onGoingMission;
        for (int i = 0; i < mission[currentMission].ruteSignsMedium.Length; i++)
        {
            mission[currentMission].ruteSignsMedium[i].SetActive(false);

        }
        for (int i = 0; i < mission[currentMission].ruteSignsMedium.Length; i++)
        {
            mission[currentMission].ruteSignsMedium[i].SetActive(false);

        }
        for (int i = 0; i < mission[currentMission].ruteSignsHard.Length; i++)
        {
            mission[currentMission].ruteSignsHard[i].SetActive(false);


        }
       
        if (currentPlayerMoney == 0)
        {
            currentPlayerMoney = mission[currentMission].reward;
        }
        else
        {
            currentPlayerMoney += mission[currentMission].reward;
        }
        mission[currentMission].missionBrienfing.SetActive(false);
        mission[currentMission].arrivingPoint.SetActive(false);
        mission[currentMission].departPoint.SetActive(false);
        _countdown.SetActive(false);
        missionIndex++;
        if(missionIndex >= mission.Capacity)
        {
            missionIndex--;
        }
        mission[currentMission].missionBrienfing.SetActive(true);
    }
    #endregion
    #region Cheats
    public void ResetPlayer()
    {
        player.transform.position = resetPoint.position;
    }
    public void MoneyCheat()
    {
        currentPlayerMoney += 100;
    }
    #endregion

}
