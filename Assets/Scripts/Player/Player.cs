using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : Killable
{
    public Camera cam;
    Transform camTrans;
    public Transform Gun;
    public Transform BulletPrefab;
    public Text Tooltip;
    public float Reach = 2;
    public float BulletSpeed = 20;
    public float FireTimeDelay = 0.2f;
    public FragmentEnum[] FragmentEnums;

    public int Stress = 0;

    private List<Fragment> fragnents;
    private float lastFireTime=0;

    protected override void Start()
    {
        GlobalState.Instance.Player = GetComponent<Transform>();
        camTrans = cam.GetComponent<Transform>();
        Time.fixedDeltaTime = 0.017f;
        fragnents = new List<global::Fragment>();
        foreach (FragmentEnum frag in FragmentEnums)
        {
            switch (frag)
            {
                case FragmentEnum.Speed:
                    {
                        Speed addFrag = gameObject.AddComponent<Speed>();
                        addFrag.enabled = false;
                        fragnents.Add(addFrag);
                    }
                    break;
                case FragmentEnum.Presence:
                    break;
                case FragmentEnum.Perseption:
                    break;
                default:
                    break;
            }
        }
        base.Start();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, Reach))
        {
            Interactable[] interacts = hit.collider.GetComponentsInParent<Interactable>();
            if (interacts.Length > 0)
            {
                Tooltip.text = "Press \"E\" to " + interacts[0].Action;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interacts[0].Activate();
                }
            }
            else
            {
                Tooltip.text = "";
            }
        }
        else
        {
            Tooltip.text = "";
        }
        if (Input.GetMouseButtonDown(0)&&Time.time>lastFireTime+FireTimeDelay)
        {
            Listener.MakeSound(transform.position,50);
            RaycastHit fireHit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out fireHit))
            {
                Transform bullet = Instantiate(BulletPrefab);
                bullet.position = Gun.position - Gun.up * 0.2f;
                bullet.LookAt(fireHit.point);
                bullet.Rotate(-90, 0, 0);
                bullet.GetComponent<Rigidbody>().velocity = camTrans.forward * BulletSpeed;
            }
            else
            {
                Transform bullet = Instantiate(BulletPrefab);
                bullet.position = Gun.position - Gun.up * 0.2f;
                bullet.rotation = Gun.rotation;
                bullet.GetComponent<Rigidbody>().velocity = camTrans.forward * BulletSpeed;
            }
            lastFireTime = Time.time;
        }
        for (int i = 0; i < fragnents.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                fragnents[i].Enabled = !fragnents[i].Enabled;
            }
        }
    }

    protected override void Die()
    {
        
    }
}
