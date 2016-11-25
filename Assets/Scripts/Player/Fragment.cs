using System;
using System.Collections.Generic;
using UnityEngine;

public enum FragmentTypeEnum
{
    Speed,
    Presence,
    Perseption,
    Heal
}
public class Fragment : MonoBehaviour
{
    public string FragmentName;
    public Sprite Icon;
    public Color Color;
    public float PassiveStress;
    public float ActiveStress;
    public FragmentTypeEnum FragmentType;

    private bool _enabled;
    public bool Enabled
    {
        get { return _enabled; }
        set
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
    private bool _active;
    public bool Active
    {
        get { return _active; }
        set
        {
            _active = value;
            if (_active)
            {
                activate();
            }
            else
            {
                deactivate();
            }
        }
    }
    public float Stress
    {
        get
        {
            float passive = _enabled ? PassiveStress : 0;
            float active = _active ? ActiveStress : 0;
            return (passive + active)/Affinity;
        }
    }
    public float Affinity { get; set; }

    public Fragment() : base()
    {
        Affinity = 1;
    }

    protected virtual void Start()
    {
        Icon = Resources.Load<Sprite>("PowerIcons/" + FragmentName);
    }

    protected virtual void enable()
    {

    }
    protected virtual void disable()
    {
        Active = false;
    }
    protected virtual void activate()
    {

    }
    protected virtual void deactivate()
    {
        
    }
}
