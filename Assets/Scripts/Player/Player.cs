using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : Killable
{
    public Camera cam;
    Transform camTrans;
    public Transform Gun;
    public Transform BulletPrefab;
    public RectTransform HUD;
    public RectTransform FragmentIcon;
    public Text Tooltip;
    public float Reach = 2;
    public float BulletSpeed = 20;
    public float FireTimeDelay = 0.2f;
    public bool NotCloaked = true;
    public FragmentTypeEnum[] FragmentEnums;

    public float StressRecoveryRate = 0.2f;
    public float Stress = 0;

    private List<Fragment> fragments;
    private float lastFireTime = 0;

    void Awake()
    {
        fragments = new List<global::Fragment>();
        foreach (FragmentTypeEnum frag in FragmentEnums)
        {
            switch (frag)
            {
                case FragmentTypeEnum.Speed:
                    {
                        Speed addFrag = gameObject.AddComponent<Speed>();
                        addFrag.enabled = false;
                        fragments.Add(addFrag);
                    }
                    break;
                case FragmentTypeEnum.Presence:
                    {
                        Presence addFrag = gameObject.AddComponent<Presence>();
                        addFrag.enabled = false;
                        fragments.Add(addFrag);
                    }
                    break;
                case FragmentTypeEnum.Perseption:
                    break;
                default:
                    break;
            }
        }
    }
    protected override void Start()
    {
        GlobalState.Instance.Player = GetComponent<Transform>();
        camTrans = cam.GetComponent<Transform>();
        Time.fixedDeltaTime = 0.017f;
        for (int i = 0; i < fragments.Count; i++)
        {
            RectTransform icon = (RectTransform)Instantiate(FragmentIcon, HUD);
            icon.GetComponent<Image>().sprite = fragments[i].Icon;
            icon.FindChild("Text").GetComponent<Text>().text = (i + 1).ToString();
            icon.anchoredPosition = new Vector3(36 * (i + 1) + 50 * i, 36, 0);
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
        if (Input.GetMouseButtonDown(0) && Time.time > lastFireTime + FireTimeDelay)
        {
            if (NotCloaked)
            {
                Listener.MakeSound(transform.position, 50);
            }
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
        int activeCount = 0;
        for (int i = 0; i < fragments.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                fragments[i].Enabled = !fragments[i].Enabled;
            }
            if (fragments[i].Enabled)
            {
                Stress += fragments[i].Stress;
                activeCount++;
            }
        }
        if (activeCount > 1)
        {
            Stress += (activeCount - 1) * 0.1f;
        }
        Stress -= StressRecoveryRate;
        if (Stress > 100)
        {
            Die();
        }
        else
        {
            if (Stress < 0)
            {
                Stress = 0;
            }
        }
    }

    protected override void Die()
    {

    }
}
