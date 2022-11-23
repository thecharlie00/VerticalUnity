using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _GAME_MANAGER;
    public float turboPower;
    public float turboPowerReamining;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurboOn()
    {
        turboPowerReamining-=Time.deltaTime;
        if(turboPowerReamining <= 0)
        {
            turboPowerReamining = 0;
        }
    }

    public void TurboOff()
    {
        turboPowerReamining += Time.deltaTime;
        if(turboPowerReamining >= turboPower)
        {
            turboPowerReamining = turboPower;
        }
    }
}
