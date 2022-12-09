using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        turboPowerReamining = turboPower;
        missionBriefing.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {
       
        if (turboPowerReamining >= 1)
        {
            theresTurboRemaining = true;
        }
        if (turboPowerReamining >= turboPower)
        {
            turboPowerReamining = turboPower;
        }
        playerMoney.text = currentPlayerMoney.ToString();
        if (!ruteSelected)
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
        missionBriefing.SetActive(false);
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
        mission[currentMission] = onGoingMission;

    }
    public void InitMission()
    {
        if (missionIndex < 0)
        {
            missionIndex++;  
        }
        if (mission[currentMission].isCompleted)
        {
            missionIndex++;
        }
        currentMission = missionIndex;
        missionTitle.text = mission[currentMission].missionTitle;
        missionDescription.text = mission[currentMission].descriptionTitle;
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
        if (currentPlayerMoney > 0)
        {
            currentPlayerMoney = mission[currentMission].reward;
        }
        else
        {
            currentPlayerMoney += mission[currentMission].reward;
        }
    }
}
