using UnityEngine;
using System.Collections;

public class FragmentObject : MonoBehaviour
{
    public FragmentTypeEnum FragmentType;

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
