using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;    // Prefab del proyectil
    public Transform firePoint;            // Punto desde donde se disparar� el proyectil
    public float projectileSpeed = 10f;    // Velocidad del proyectil
    public AudioClip bulletClip;
    public float sfxVolumen;

    public void OnAttack() 
    {
        Shoot();
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instanciar el proyectil en el punto de disparo
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            AudioManager.Instance.PlaySFX(bulletClip, sfxVolumen);

            // Anadir movimiento al proyectil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * projectileSpeed;
            }
        }
    }
}
