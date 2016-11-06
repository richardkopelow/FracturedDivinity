using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Player : Killable
{
    public Camera Cam;
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
    public bool Unkillable = false;
    public FragmentTypeEnum[] FragmentEnums;

    public float StressRecoveryRate = 0.2f;
    public float Stress = 0;

    ScreenOverlay stressOverlay;
    ScreenOverlay healthOverlay;

    private List<Fragment> fragments;
    private List<GameObject> fragmentIcons;
    private float lastFireTime = 0;
    private float multiFragmentAffinity = 1;

    void Awake()
    {
        fragments = new List<Fragment>();
        foreach (FragmentTypeEnum frag in FragmentEnums)
        {
            addFrag(frag);
        }
    }
    protected override void Start()
    {
        GlobalState.Instance.Player = GetComponent<Transform>();
        camTrans = Cam.GetComponent<Transform>();
        ScreenOverlay[] overlays = camTrans.GetComponents<ScreenOverlay>();
        stressOverlay = overlays[0];
        healthOverlay = overlays[1];
        Time.fixedDeltaTime = 0.017f;
        fragmentIcons = new List<GameObject>();
        StartCoroutine(buildFragmentIcons());
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
                float fragStress = fragments[i].Stress;
                fragments[i].Affinity += fragStress / 100000;
                Stress += fragStress;
                activeCount++;
            }
        }
        if (activeCount > 1)
        {
            float multiFragmentStress = (activeCount - 1) * 0.1f;
            multiFragmentAffinity = multiFragmentStress / 100000;
            Stress += multiFragmentStress;
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
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        stressOverlay.intensity = Stress / 100;
        healthOverlay.intensity = (MaxHealth - Health) / (float)MaxHealth;
    }

    public void TakeOnFragment(FragmentTypeEnum fragmentType)
    {
        addFrag(fragmentType);
        StartCoroutine(buildFragmentIcons());
    }

    private void addFrag(FragmentTypeEnum frag)
    {
        Fragment addFrag=null;
        switch (frag)
        {
            case FragmentTypeEnum.Speed:
                {
                    addFrag = gameObject.AddComponent<Speed>();
                    fragments.Add(addFrag);
                }
                break;
            case FragmentTypeEnum.Presence:
                {
                    addFrag = gameObject.AddComponent<Presence>();
                    fragments.Add(addFrag);
                }
                break;
            case FragmentTypeEnum.Perseption:
                break;
            case FragmentTypeEnum.Heal:
                {
                    addFrag = gameObject.AddComponent<Heal>();
                    fragments.Add(addFrag);
                }
                break;
            default:
                break;
        }
        addFrag.FragmentName = frag.ToString();
    }

    private IEnumerator buildFragmentIcons()
    {
        yield return null;
        foreach (Fragment frag in fragments)
        {
            frag.Enabled = false;
        }
        foreach (GameObject icon in fragmentIcons)
        {
            Destroy(icon);
        }
        fragmentIcons = new List<GameObject>();
        for (int i = 0; i < fragments.Count; i++)
        {
            RectTransform icon = (RectTransform)Instantiate(FragmentIcon, HUD);
            icon.GetComponent<Image>().sprite = fragments[i].Icon;
            icon.FindChild("Text").GetComponent<Text>().text = (i + 1).ToString();
            icon.anchoredPosition = new Vector3(36 * (i + 1) + 50 * i, 36, 0);
            fragmentIcons.Add(icon.gameObject);
        }
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0 && !Unkillable)
        {
            Die();
        }
    }
    protected override void Die()
    {

    }
}
