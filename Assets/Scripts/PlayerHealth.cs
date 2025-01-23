using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private HealthBar _ownHealthBar;

    [SerializeField] Canvas _dieCanvas;
    [SerializeField] Canvas _winnerCanvas;

    public void Awake()
    {
       health = maxHealth;
    }

    private void Update()
    {
        _ownHealthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(float damage) 
    { 
        health -= damage;
        _healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0) 
        {
            _dieCanvas.gameObject.SetActive(true);
            _winnerCanvas.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void RepareDamage( ) 
    { 
        health = maxHealth;
        _healthBar.UpdateHealthBar(health, maxHealth);
    }


}
