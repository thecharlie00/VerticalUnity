using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _GAME_MANAGER;
    #region Turbo
    public float turboPower;
    public float turboPowerReamining;
    public bool theresTurboRemaining;
    public bool isActive;
    #endregion
    #region Mission
    public GameObject missionBrienfing;
    public float reward;
    public GameObject[] ruteSignsEasy;
    public GameObject[] ruteSignsMedium;
    public GameObject[] ruteSignsHard;
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
       missionBrienfing.SetActive(false);
        for (int i = 0; i < ruteSignsEasy.Length; i++)
        {
            ruteSignsEasy[i].SetActive(false);
        }
        for (int i = 0; i < ruteSignsMedium.Length; i++)
        {
            ruteSignsMedium[i].SetActive(false);
        }
        for (int i = 0; i < ruteSignsHard.Length; i++)
        {
            ruteSignsHard[i].SetActive(false);
        }

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
        for(int i =0; i < ruteSignsEasy.Length - 1; i++)
        {
            ruteSignsEasy[i].SetActive(true);
        }
        reward = 100f;
   }
}
