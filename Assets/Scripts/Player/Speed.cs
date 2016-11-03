using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

class Speed : Fragment
{
    RigidbodyFirstPersonController controller;
    Player player;
    float forwardSpeed;
    float strafeSpeed;
    float backwardsSpeed;
    float originalFireTimeDelay;

    void Start()
    {
        controller = GetComponent<RigidbodyFirstPersonController>();
        player = GetComponent<Player>();
        forwardSpeed = controller.movementSettings.ForwardSpeed;
        strafeSpeed = controller.movementSettings.StrafeSpeed;
        backwardsSpeed = controller.movementSettings.BackwardSpeed;
        originalFireTimeDelay = player.FireTimeDelay;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Active = !Active;
            if (Active)
            {
                Time.timeScale=0.1f;
                Time.fixedDeltaTime = GlobalState.StandardFixedDeltaTime * Time.timeScale;
                controller.movementSettings.ForwardSpeed = forwardSpeed * 2;
                controller.movementSettings.StrafeSpeed = strafeSpeed * 2;
                controller.movementSettings.BackwardSpeed = backwardsSpeed * 2;
                player.FireTimeDelay = originalFireTimeDelay / 2;
            }
            else
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = GlobalState.StandardFixedDeltaTime;
                controller.movementSettings.ForwardSpeed = forwardSpeed;
                controller.movementSettings.StrafeSpeed = strafeSpeed;
                controller.movementSettings.BackwardSpeed = backwardsSpeed;
                player.FireTimeDelay = originalFireTimeDelay;
            }
        }
    }
}
