using System;
using System.Collections.Generic;
using UnityEngine;

class GlobalState
{
    public const float StandardFixedDeltaTime = 0.02f;

    private static GlobalState _instance;

    public static GlobalState Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GlobalState();
            }
            return _instance;
        }
    }

    public Transform Player;

    public bool DidOffice
    {
        get
        {
            return PlayerPrefs.GetInt("DidOffice") == 1;
        }
        set
        {
            PlayerPrefs.SetInt("DidOffice", value ? 1 : 0);
        }
    }

    private GlobalState()
    {

    }
}
