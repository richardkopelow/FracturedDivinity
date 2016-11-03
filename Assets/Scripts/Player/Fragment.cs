using System;
using System.Collections.Generic;
using UnityEngine;

public enum FragmentEnum
{
    Speed,
    Presence,
    Perseption
}
public class Fragment : MonoBehaviour
{
    public Texture Icon;
    public Color Color;
    public float PassiveStress;
    public float ActiveStress;

    private bool _enabled;
    public bool Enabled
    {
        get { return _enabled; }
        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                if (_enabled)
                {
                    enable();
                }
                else
                {
                    disable();
                }
                enabled = _enabled;
            }
        }
    }
    private bool _active;
    public bool Active
    {
        get { return _active; }
        set { _active = value; }
    }
    public float Stress
    {
        get
        {
            float passive = _enabled ? PassiveStress : 0;
            float active = _active ? ActiveStress : 0;
            return passive + active;
        }
    }


    protected virtual void enable()
    {

    }
    protected virtual void disable()
    {

    }
}
