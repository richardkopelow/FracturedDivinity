using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MapInteraction : MonoBehaviour
{
    public Camera Cam;
    Transform camTrans;
    public Text Tooltip;
    public Transform MapTarget;

    private bool tooltipSet = false;
    private bool viewingMap = false;
    private Vector3 playerHeadPosition;

    void Start()
    {
        camTrans = Cam.GetComponent<Transform>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 4f))
        {
            if (hit.collider.name == "Map")
            {
                tooltipSet = true;
                string action = viewingMap ? "leave" : "look at";
                Tooltip.text = "Click to " + action + " map";
                if (Input.GetMouseButtonDown(0))
                {
                    if (viewingMap)
                    {
                        StartCoroutine(setCamera(1.5f));
                    }
                    else
                    {
                        playerHeadPosition = camTrans.position;
                        StartCoroutine(setCamera(1.5f));
                    }
                }
            }
            else
            {
                if (tooltipSet)
                {
                    tooltipSet = false;
                    Tooltip.text = "";
                }
            }
        }
        else
        {
            if (tooltipSet)
            {
                tooltipSet = false;
                Tooltip.text = "";
            }
        }
    }

    IEnumerator setCamera(float finishTime)
    {
        if (!viewingMap)
        {
            GetComponent<RigidbodyFirstPersonController>().enabled = false;
            GetComponent<Player>().enabled = false;
        }
        Vector3 initialPosition = viewingMap ? MapTarget.position : playerHeadPosition;
        Vector3 finalPosition = viewingMap ? playerHeadPosition : MapTarget.position;
        float time = 0;
        while (time < finishTime)
        {
            camTrans.position = Vector3.Slerp(initialPosition, finalPosition, time / finishTime);
            camTrans.LookAt(camTrans.position + MapTarget.forward);
            time += Time.deltaTime;
            yield return null;
        }
        if (viewingMap)
        {
            GetComponent<RigidbodyFirstPersonController>().enabled = true;
            GetComponent<Player>().enabled = true;
        }
        Cursor.lockState = viewingMap ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !viewingMap;
        viewingMap = !viewingMap;
    }
}
