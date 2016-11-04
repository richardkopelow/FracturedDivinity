using UnityEngine;
using System.Collections;

public class Grunt : Killable
{
    public Transform BulletPrefab;
    public Transform Gun;
    public float FireDelay = 0.5f;

    Transform trans;
    NavMeshAgent navAgent;
    float lastFireTime;

    #region SoundStuff
    public float SoundThreshold = 1;
    float soundIntensity = 0;
    Vector3 soundLocation;
    #endregion

    protected override void Start()
    {
        trans = GetComponent<Transform>();
        navAgent = GetComponent<NavMeshAgent>();
        GetComponent<Listener>().OnHeardSound += Grunt_OnHeardSound;
        base.Start();
    }

    private void Grunt_OnHeardSound(Vector3 location, float intensity)
    {
        if (intensity > SoundThreshold)
        {
            if (intensity > soundIntensity)
            {
                soundIntensity = intensity;
                soundLocation = location;
            }
        }
    }

    void Update()
    {
        int angle;
        if (canSeePlayer(out angle))
        {
            trans.Rotate(0, Mathf.Lerp(0, angle, Time.deltaTime * 3), 0);
            if ((GlobalState.Instance.Player.position - trans.position).magnitude > 3)
            {
                navAgent.SetDestination(GlobalState.Instance.Player.position);
            }
            else
            {
                navAgent.ResetPath();
            }
            if (Time.time > lastFireTime + FireDelay)
            {
                Vector3 fireDirection = (GlobalState.Instance.Player.position - trans.position).normalized;
                Transform bullet = Instantiate(BulletPrefab);
                bullet.position = Gun.position - Gun.up * 0.2f;
                bullet.rotation = Gun.rotation;
                bullet.GetComponent<Rigidbody>().velocity = fireDirection * 30;
                lastFireTime = Time.time;
            }
            soundIntensity = 0;
        }
        else
        {
            if (soundIntensity > 0)
            {
                navAgent.destination = soundLocation;
            }
            if ((trans.position - soundLocation).magnitude < 3)
            {
                soundIntensity = 0;
            }
        }
    }

    private bool canSeePlayer(out int angle)
    {
        RaycastHit hit;
        for (angle = -35; angle <= 35; angle++)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * trans.forward;
            if (Physics.Raycast(trans.position, direction, out hit))
            {
                Player player = hit.collider.GetComponent<Player>();
                if (player != null && player.NotCloaked)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
