using UnityEngine;

public class BossMissile : MonoBehaviour
{
    public GameObject missilePrefab; // Préfabriqué du missile avec effet de particules
    public Transform firePoint; // Point de tir du missile
    public float missileSpeed = 10f; // Vitesse du missile
    public float fireInterval = 1f; // Intervalle entre les tirs en secondes
    public Transform player; // Référence au joueur

    private float fireTimer;

    private void Start()
    {
        fireTimer = fireInterval;

        // Vérification des références au démarrage
        if (missilePrefab == null)
        {
            Debug.LogError("Le préfabriqué de missile n'est pas assigné !");
        }
        if (firePoint == null)
        {
            Debug.LogError("Le point de tir n'est pas assigné !");
        }
        if (player == null)
        {
            Debug.LogError("Le transform du joueur n'est pas assigné !");
        }
    }

    private void Update()
    {
        // Décompte du timer
        fireTimer -= Time.deltaTime;

        // Si le timer atteint 0, lancer un missile
        if (fireTimer <= 0f)
        {
            LaunchMissile();
            fireTimer = fireInterval; // Réinitialiser le timer
        }
    }

    void LaunchMissile()
    {
        if (player == null)
        {
            Debug.LogError("La référence au joueur n'est pas assignée !");
            return;
        }

        Vector2 playerPosition = player.position;
        Debug.Log($"Position du joueur au moment du tir: {playerPosition}");

        Vector2 direction = (playerPosition - (Vector2)firePoint.position).normalized;
        Debug.Log($"Direction calculée: {direction}");

        GameObject missile = Instantiate(missilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * missileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        missile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Debug.Log($"Missile tiré vers {playerPosition} avec une direction {direction}");
    }
}
