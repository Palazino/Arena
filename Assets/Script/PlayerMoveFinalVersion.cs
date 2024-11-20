using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Vitesse de déplacement

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        // Détecter le clic de la souris
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector2(touchPosition.x, touchPosition.y);
            isMoving = true;
            Debug.Log($"Nouvelle position cible : {targetPosition}"); // Log de la nouvelle position cible
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

            // Arrêter le mouvement lorsque la position cible est atteinte
            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }

            Debug.Log($"Position actuelle du joueur : {rb.position}"); // Log de la position actuelle du joueur
        }
    }
}
