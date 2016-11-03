using UnityEngine;
using System.Collections;

public class Door : Interactable
{
    Animation anim;

    bool open = false;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public override void Activate()
    {
        if (!anim.isPlaying)
        {
            if (open)
            {
                Action = "Open";
                anim.Play("Close");
            }
            else
            {
                Action = "Close";
                anim.Play("Open");
            }
            open = !open;
        }
        base.Activate();
    }
}
