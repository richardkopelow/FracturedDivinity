using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Presence : Fragment
{
    Player player;
    RigidbodyFirstPersonController controller;

    void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<RigidbodyFirstPersonController>();

        PassiveStress = 0.15f;
        ActiveStress = 0.1f;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Active = !Active;
        }
    }
    protected override void activate()
    {
        player.NotCloaked = false;
        controller.Noisy = false;
    }
    protected override void deactivate()
    {
        player.NotCloaked = true;
        controller.Noisy = true;
    }
}
