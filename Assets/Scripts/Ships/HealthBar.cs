using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public ShipData shipData;
    public Slider healthBar;


    private void Start()
    {
        healthBar.maxValue = shipData.MaxHealth;
        healthBar.value = healthBar.maxValue;
        shipData.onDamage += UpdateHealth;
    }

    private void Update()
    {
        transform.position = shipData.transform.GetChild(0).position;
    }

    private void UpdateHealth(int value)
    {
        if (value <= 0) Destroy(gameObject);
        healthBar.value = value;
    }

    private void OnDisable()
    {
        shipData.onDamage -= UpdateHealth;
    }
}
