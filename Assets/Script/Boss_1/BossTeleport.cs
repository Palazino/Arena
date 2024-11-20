using System.Collections;
using UnityEngine;

public class BossTeleportation : MonoBehaviour
{
    public Transform[] teleportationPoints; // Liste des points de téléportation
    public float teleportDelay = 2f; // Temps entre chaque téléportation
    public float fadeDuration = 0.5f; // Durée de l'animation de disparition/réapparition
    public Vector3 deformationFactor = new Vector3(1.5f, 0.3f, 1f); // Facteur de déformation (x, y, z)

    private SpriteRenderer spriteRenderer; // Référence au SpriteRenderer du boss
    private Vector3 originalScale; // Échelle originale du boss
    private int currentPointIndex = 0; // Indice du point actuel de téléportation

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Récupère le SpriteRenderer
        originalScale = transform.localScale; // Sauvegarde l'échelle originale
        StartCoroutine(TeleportRoutine()); // Lance la routine de téléportation
    }

    IEnumerator TeleportRoutine()
    {
        while (true)
        {
            // Disparition et déformation du boss (fade out + deformation)
            yield return StartCoroutine(FadeOutAndDeform());

            // Téléportation vers le prochain point
            transform.position = teleportationPoints[currentPointIndex].position;

            // Réapparition du boss (fade in + restauration de la déformation)
            yield return StartCoroutine(FadeInAndRestore());

            // Passer au point suivant
            currentPointIndex = (currentPointIndex + 1) % teleportationPoints.Length;

            // Attendre avant la prochaine téléportation
            yield return new WaitForSeconds(teleportDelay);
        }
    }

    IEnumerator FadeOutAndDeform()
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;
        Vector3 initialScale = transform.localScale;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            Vector3 scale = Vector3.Lerp(initialScale, deformationFactor, elapsedTime / fadeDuration); // Déformation progressive

            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            transform.localScale = scale; // Applique la déformation

            yield return null;
        }

        // Assurez-vous que l'alpha et l'échelle soient à leurs valeurs finales
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        transform.localScale = deformationFactor; // Applique la déformation finale
    }

    IEnumerator FadeInAndRestore()
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;
        Vector3 initialScale = transform.localScale;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            Vector3 scale = Vector3.Lerp(initialScale, originalScale, elapsedTime / fadeDuration); // Restauration progressive

            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            transform.localScale = scale; // Restaure l'échelle

            yield return null;
        }

        // Assurez-vous que l'alpha et l'échelle soient à leurs valeurs finales
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
        transform.localScale = originalScale; // Restaure l'échelle originale
    }
}
