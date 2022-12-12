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
        public float waitTime;
        public float reward;
        public GameObject[] ruteSignsEasy;
        public GameObject[] ruteSignsMedium;
        public GameObject[] ruteSignsHard;
        public bool isCompleted;
    }
    [NonReorderable]
    public List<Missions> mission = new List<Missions>();
    public Text missionTitle;
    public Text missionDescription;
    public GameObject missionBriefing;
    public float _time_;
    public bool isWaiting;
    bool ruteSelected;
    #endregion
    #region Player
    public float currentPlayerMoney;
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
        if(Menu != null && Options != null && Credits != null)
        {
            Menu.SetActive(true);
            Options.SetActive(false);
            Credits.SetActive(false);
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
            for(int j =0; j < mission[i].ruteSignsEasy.Length; j++)
            {
                mission[i].ruteSignsEasy[j].SetActive(false);
            }
            for (int k = 0; k < mission[i].ruteSignsMedium.Length; k++)
            {
                mission[i].ruteSignsMedium[k].SetActive(false);
            }
            for (int l = 0; l < mission[i].ruteSignsHard.Length; l++)
            {
                mission[i].ruteSignsHard[l].SetActive(false);
            }
        }
       

    }
    void Update()
    {
        if(sceneName == "SplashScreen")
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("MenuPrincipal");
            }
        }
        if (missionIndex < 0)
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
                mission[currentMission].ruteSignsEasy[i].SetActive(false);

            }
            for (int i = 0; i < mission[currentMission].ruteSignsMedium.Length; i++)
            {
                mission[currentMission].ruteSignsMedium[i].SetActive(false);

            }
            for (int i = 0; i < mission[currentMission].ruteSignsHard.Length; i++)
            {
                mission[currentMission].ruteSignsHard[i].SetActive(false);

            }
        }
        if(ruteSelected && !isWaiting)
        {
           
            mission[currentMission].departPoint.SetActive(true);
        }
        
       
        if (isWaiting)
        {
            
            ScapeWaiting();
        }


    }
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
        missionBriefing.SetActive(true);

    }
    public void ScapeWaiting()
    {
        
          
        
        
        if (isWaiting)
        {
            
            _time_ -= Time.deltaTime * 1000;
        }
        if(_time_ <= 0)
        {
            /*isWaiting = false;
            mission[currentMission].arrivingPoint.SetActive(true);*/
            //
            _time_ = 0;
            mission[currentMission].arrivingPoint.SetActive(true);
            isWaiting = false;
            


        }
    }
    public void EndMission()
    {
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
        missionIndex++;
        if(missionIndex >= mission.Capacity)
        {
            missionIndex--;
        }
        mission[currentMission].missionBrienfing.SetActive(true);
    }
}
