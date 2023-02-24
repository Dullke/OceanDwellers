using Sirenix.OdinInspector;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    [SerializeField] [BoxGroup("Required Data")] private Sprite damagedSprite;
    [SerializeField] [BoxGroup("Required Data")] private Sprite criticalSprite;
    [SerializeField] [TitleGroup("Required Data/Components")] private SpriteRenderer shipGraphics;
    [SerializeField] [TitleGroup("Required Data/Objects")] private GameObject fire;
    [SerializeField] [TitleGroup("Required Data/Objects")] private GameObject explosion;
    [SerializeField] [TitleGroup("Required Data/Objects")] private GameObject destroyedShip;

    [SerializeField] [BoxGroup("Feedback properties")] private int damagedHealth;
    [SerializeField] [BoxGroup("Feedback properties")] private int criticalHealth;

    private ShipData data;

    private void Awake()
    {
        data = GetComponent<ShipData>();
    }

    private void SpawnDamageFeedback(Sprite shipState)
    {
        shipGraphics.sprite = shipState;

        Vector3 firePosition = shipGraphics.transform.position + new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f));
        Instantiate(fire, firePosition, Quaternion.identity, transform);
    }

    private void DestroyShip()
    {
        GameObject explosionObject = Instantiate(explosion, shipGraphics.transform.position, Quaternion.identity);
        explosionObject.transform.localScale = Vector3.one;
        Destroy(explosionObject, 2f);

        GameObject destroyedShipObject = Instantiate(destroyedShip, transform.position, Quaternion.Euler(transform.eulerAngles));
        Destroy(destroyedShipObject, 10f);

        Destroy(gameObject);
    }

    private void Feedback(int currentHealth)
    {
        

        if (currentHealth <= 0)
        {
            DestroyShip();
            return;
        }

        if (currentHealth <= criticalHealth)
        {
            SpawnDamageFeedback(criticalSprite);
            return;
        }

        if (currentHealth <= damagedHealth)
        {
            SpawnDamageFeedback(damagedSprite);
            return;
        }

    }

    private void OnEnable()
    {
        data.onDamage += Feedback;
    }

    private void OnDisable()
    {
        data.onDamage -= Feedback;
    }
}
