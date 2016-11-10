using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour
{
    public Light CameraLight;

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && player.NotCloaked)
        {
            Listener.MakeSound(player.GetComponent<Transform>().position, 40);
            CameraLight.color = Color.red;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        CameraLight.color = Color.green;
    }
}
