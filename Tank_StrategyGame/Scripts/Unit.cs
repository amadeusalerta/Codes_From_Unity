using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float maxHealth;
    public Image healthFill;

    public float currentHealth;

    public virtual void Awake()
    {
        currentHealth = maxHealth;
        if(healthFill)healthFill.fillAmount = 1;
    }

    public virtual void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }

        if(healthFill)healthFill.fillAmount = currentHealth / maxHealth;
    }
}