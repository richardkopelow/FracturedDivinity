using UnityEngine;
using System.Collections;

public class FragmentObject : MonoBehaviour
{
    public FragmentTypeEnum FragmentType;

    void Start()
    {
        if (GlobalState.Instance.Player.GetComponent<Player>().FragmentTypeEnums.Contains(FragmentType))
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            player.TakeOnFragment(FragmentType);
            Destroy(gameObject);
        }
    }
}
