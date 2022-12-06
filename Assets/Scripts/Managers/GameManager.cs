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
    [System.Serializable]
    public struct Missions
    {
        public string missionTitle;
        public string descriptionTitle;
        public GameObject missionBrienfing;
        public GameObject departPoint;
        public GameObject arrivingPoint;
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
        else
        {
            mission[currentMission].departPoint.SetActive(true);
        }
        
        mission[currentMission].arrivingPoint.SetActive(false);


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
   {
        ruteSelected = true;
        for(int i = 0; i <mission[currentMission].ruteSignsEasy.Length; i++)
        {
            mission[currentMission].ruteSignsEasy[i].SetActive(true);

        }
        Time.timeScale = 1;
        missionBriefing.SetActive(false);
        mission[currentMission].missionBrienfing.SetActive(false);
    }

    public void Rute2()
    {
        ruteSelected = true;
        for (int i = 0; i < mission[currentMission].ruteSignsMedium.Length; i++)
        {
            mission[currentMission].ruteSignsMedium[i].SetActive(true);

        }
        Time.timeScale = 1;
        missionBriefing.SetActive(false);
        mission[currentMission].missionBrienfing.SetActive(false);
    }
    public void Rute3()
    {
        ruteSelected = true;
        for (int i = 0; i < mission[currentMission].ruteSignsHard.Length; i++)
        {
            mission[currentMission].ruteSignsHard[i].SetActive(true);

        }
        Time.timeScale = 1;
        missionBriefing.SetActive(false);
        mission[currentMission].missionBrienfing.SetActive(false);
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
}
