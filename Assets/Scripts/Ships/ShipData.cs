using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ShipData : MonoBehaviour, IDamageable
{
    [SerializeReference] [BoxGroup("Ship Properties")] private int maxHealth = 10;

    public int MaxHealth { get { return maxHealth; } }

    public event UnityAction<int> onDamage;


    private int health;

    protected virtual void Awake()
    {
        health = maxHealth;
    }

    public void Damage(int damage)
    {
        health -= damage;
        onDamage?.Invoke(health);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Damage(100);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
