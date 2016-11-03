using System;
using System.Collections.Generic;
using UnityEngine;

class GlobalState
{
    public const float StandardFixedDeltaTime = 0.02f;

    private static GlobalState _instance;

    public static GlobalState Instance
    {
        get {
            if (_instance == null)
            {
                _instance = new GlobalState();
            }
            return _instance;
        }
    }

    public Transform Player;

    private GlobalState()
    {

    }
}
