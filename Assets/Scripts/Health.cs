using UnityEngine;

public class Health : MonoBehaviour, IDamageble
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
    }



    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Damage: {damage}, Current Health: {currentHealth}");
        if (currentHealth <= 0f)
            Destroy(gameObject);
    }

    
}
