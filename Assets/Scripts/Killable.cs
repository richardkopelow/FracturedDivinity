using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    protected virtual void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health<=0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
