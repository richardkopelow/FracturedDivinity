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

        PassiveStress = 0.15f;
        ActiveStress = 0.2f;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Active = !Active;
        }
    }
    protected override void activate()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = GlobalState.StandardFixedDeltaTime * Time.timeScale;
        controller.movementSettings.ForwardSpeed = forwardSpeed * 2;
        controller.movementSettings.StrafeSpeed = strafeSpeed * 2;
        controller.movementSettings.BackwardSpeed = backwardsSpeed * 2;
        player.FireTimeDelay = originalFireTimeDelay / 2;
    }
    protected override void deactivate()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = GlobalState.StandardFixedDeltaTime;
        controller.movementSettings.ForwardSpeed = forwardSpeed;
        controller.movementSettings.StrafeSpeed = strafeSpeed;
        controller.movementSettings.BackwardSpeed = backwardsSpeed;
        player.FireTimeDelay = originalFireTimeDelay;
    }
}
