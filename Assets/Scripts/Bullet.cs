using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Killable killable = collision.collider.GetComponent<Killable>();
        if (killable != null)
        {
            killable.TakeDamage(2);
        }
    }
}
