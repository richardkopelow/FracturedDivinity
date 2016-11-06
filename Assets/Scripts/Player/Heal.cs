using UnityEngine;
using System.Collections;

public class Heal : Fragment
{
    Player player;

    bool _engaged = false;
    bool engaged {
        get { return _engaged; }
        set
        {
            _engaged = value;
            player.Unkillable = _engaged;
        }
    }
    float lastHealth;

    protected override void Start()
    {
        player = GetComponent<Player>();
        lastHealth = player.Health;

        PassiveStress = 0.15f;
        base.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            engaged = !engaged;
            if (engaged)
            {
                lastHealth = player.Health;
            }
        }
        if (engaged)
        {
            if (player.Health < player.MaxHealth)
            {
                Active = true;
                float healing = lastHealth - player.Health + 0.02f;
                player.Health += healing;
                ActiveStress = healing * 7 + 0.065f;
            }
            else
            {
                Active = false;
            }
        }
        else
        {
            Active = false;
        }
        lastHealth = player.Health;
    }

    protected override void disable()
    {
        engaged = false;
        base.disable();
    }

    protected override void enable()
    {
        base.enable();
    }
}