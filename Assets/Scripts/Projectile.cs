using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f; // Tiempo antes de destruir el proyectil
    public float damage = 10f;

    void Start()
    {
        // Destruir el proyectil después de 'lifeTime' segundos
        Destroy(gameObject, lifeTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2") 
        {
            // Intentamos obtener un componente que tenga el método TakeDamage
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Si lo encontramos, le hacemos daño
                playerHealth.TakeDamage(damage);
            }
        }

        // Destruir el proyectil después de la colisión
        Destroy(gameObject);
    }
}
