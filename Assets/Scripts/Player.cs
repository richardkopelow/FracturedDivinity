using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Killable
{
    public Camera cam;
    Transform camTrans;
    public Transform Gun;
    public Transform Bullet;
    public Text Tooltip;
    public float Reach = 2;
    public float BulletSpeed=20;

    public int Stress=0;

    protected override void Start()
    {
        camTrans = cam.GetComponent<Transform>();
        Time.fixedDeltaTime = 0.017f;
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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit fireHit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out fireHit))
            {
                Transform bullet = Instantiate(Bullet);
                bullet.position = Gun.position;
                bullet.LookAt(fireHit.point);
                bullet.Rotate(-90, 0, 0);
                bullet.GetComponent<Rigidbody>().velocity = camTrans.forward * BulletSpeed;
            }
            else
            {
                Transform bullet = Instantiate(Bullet);
                bullet.position = Gun.position;
                bullet.rotation = Gun.rotation;
                bullet.GetComponent<Rigidbody>().velocity = camTrans.forward * BulletSpeed;
            }
        }
    }
}
