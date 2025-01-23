using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrulla")]
    public List<Transform> waypoints; // Lista de puntos de ruta
    public float speed = 2f;         // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de rotación

    [Header("Detección")]
    public SphereCollider detectionCollider; // Collider para detección
    public string playerTag = "Player";      // Tag del jugador

    [Header("Ataque")]
    public GameObject projectilePrefab;    // Prefab del proyectil
    public Transform firePoint;            // Punto desde donde se disparar� el proyectil
    public Transform firePoint2;            // Punto desde donde se disparar� el proyectil
    public float projectileSpeed = 10f;    // Velocidad del proyectil
    public float fireRate = 1f;      // Tiempo entre disparos en segundos

    private float lastFireTime = 0f; // Última vez que se disparó
    private bool useFirstShootPoint = true;
    private bool isAttacking = false;
    private Transform targetPlayer; // Referencia al jugador detectado

    [Header("Vida")]
    public float health;
    public float maxHealth = 100;

    private int currentWaypointIndex = 0; // Índice del punto de ruta actual
    private Animator animator;   // Referencia al Animator

    void Start()
    {
        health = maxHealth;

        animator = GetComponent<Animator>();

        if (waypoints.Count == 0)
        {
            Debug.LogError("No hay puntos de ruta asignados.");
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            // Si está atacando, mirar al jugador y no moverse por los waypoints
            if (targetPlayer != null )
            {
                RotateTowards(targetPlayer.position);
                Shoot(targetPlayer.position);
            }
        }
        else
        {
            // Continuar patrullando entre los puntos de ruta
            Patrol();
            lastFireTime = 0f;
        }
    }

    private void Patrol()
    {
        if (waypoints.Count == 0) return;

        animator.SetBool("Walk", true); // Activar la animación de ataque

        // Moverse hacia el waypoint actual
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotar hacia el waypoint actual
        RotateTowards(targetWaypoint.position);

        // Verificar si se llegó al waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            // Cambiar al siguiente waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }

    private void Shoot(Vector3 targetPosition) 
    {
        if (Time.time - lastFireTime < fireRate) return;
        animator.SetTrigger("Attack");

        if (projectilePrefab != null && firePoint != null)
        {
            Transform currentShootPoint = useFirstShootPoint ? firePoint : firePoint2;

            if (targetPlayer != null)
            {
                Vector3 directionToTarget = (targetPlayer.position - currentShootPoint.position).normalized;

                // Ajustar la rotación del punto de disparo hacia el objetivo en el eje Y
                currentShootPoint.rotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, directionToTarget.y + 0.1f, directionToTarget.z));
            }

            // Instanciar el proyectil en el punto de disparo
            GameObject projectile = Instantiate(projectilePrefab, currentShootPoint.position, currentShootPoint.rotation);

            //AudioManager.Instance.PlaySFX(bulletClip, sfxVolumen);

            // Anadir movimiento al proyectil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = currentShootPoint.forward * projectileSpeed;
            }

            // Alternar entre los puntos de disparo
            useFirstShootPoint = !useFirstShootPoint;

            // Actualizar el tiempo del último disparo
            lastFireTime = Time.time;

            // (Opcional) Destruir el proyectil después de un tiempo para evitar saturación en la escena
            Destroy(projectile, 5f);
        }
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            targetPlayer = other.transform;
            isAttacking = true;
            animator.SetBool("Walk", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            targetPlayer = null;
            isAttacking = false;
            animator.SetBool("Walk", true); // Activar la animación de ataque
        }
    }
}
