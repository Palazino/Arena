using UnityEngine;

public class BossMissile : MonoBehaviour
{
    public GameObject missilePrefab; // Pr�fabriqu� du missile avec effet de particules
    public Transform firePoint; // Point de tir du missile
    public float missileSpeed = 10f; // Vitesse du missile
    public float fireInterval = 1f; // Intervalle entre les tirs en secondes
    public Transform player; // R�f�rence au joueur

    private float fireTimer;

    private void Start()
    {
        fireTimer = fireInterval;

        // V�rification des r�f�rences au d�marrage
        if (missilePrefab == null)
        {
            Debug.LogError("Le pr�fabriqu� de missile n'est pas assign� !");
        }
        if (firePoint == null)
        {
            Debug.LogError("Le point de tir n'est pas assign� !");
        }
        if (player == null)
        {
            Debug.LogError("Le transform du joueur n'est pas assign� !");
        }
    }

    private void Update()
    {
        // D�compte du timer
        fireTimer -= Time.deltaTime;

        // Si le timer atteint 0, lancer un missile
        if (fireTimer <= 0f)
        {
            LaunchMissile();
            fireTimer = fireInterval; // R�initialiser le timer
        }
    }

    void LaunchMissile()
    {
        if (player == null)
        {
            Debug.LogError("La r�f�rence au joueur n'est pas assign�e !");
            return;
        }

        Vector2 playerPosition = player.position;
        Debug.Log($"Position du joueur au moment du tir: {playerPosition}");

        Vector2 direction = (playerPosition - (Vector2)firePoint.position).normalized;
        Debug.Log($"Direction calcul�e: {direction}");

        GameObject missile = Instantiate(missilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * missileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        missile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Debug.Log($"Missile tir� vers {playerPosition} avec une direction {direction}");
    }
}
