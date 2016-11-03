using UnityEngine;
using System.Collections;

public class Listener : MonoBehaviour
{
    const float soundFalloff = 1;
    public delegate void HearSoundDelegate(Vector3 location,float intensity);
    static event HearSoundDelegate onHearSound;

    public static void MakeSound(Vector3 location,float intensity)
    {
        if (onHearSound!=null)
        {
            onHearSound(location,intensity);
        }
    }
    public event HearSoundDelegate OnHeardSound;

    Transform trans;

    void Start()
    {
        trans=GetComponent<Transform>();
        onHearSound += Listener_onHearSound;
    }

    private void Listener_onHearSound(Vector3 location, float intensity)
    {
        if (OnHeardSound != null)
        {
            OnHeardSound(location, intensity - soundFalloff * (location - trans.position).magnitude);
        }
    }

    public void OnDestroy()
    {
        onHearSound -= Listener_onHearSound;
    }
}
