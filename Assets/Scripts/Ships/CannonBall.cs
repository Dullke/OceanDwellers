using Sirenix.OdinInspector;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] [BoxGroup("Required Data")] private Rigidbody2D rigidBody;
    [SerializeField] [BoxGroup("Required Data")] private GameObject impact;
    [SerializeField] [TitleGroup("Cannon values")] private int force;

    private void Start()
    {
        rigidBody.AddForce(transform.right * force);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable ship))
        {
            ship.Damage(1);
            GameObject impactObject = Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(impactObject, 2f);
            Destroy(gameObject);
        }
    }
}
