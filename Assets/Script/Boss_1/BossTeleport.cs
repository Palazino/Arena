using System.Collections;
using UnityEngine;

public class BossTeleportation : MonoBehaviour
{
    public Transform[] teleportationPoints; // Liste des points de t�l�portation
    public float teleportDelay = 2f; // Temps entre chaque t�l�portation
    public float fadeDuration = 0.5f; // Dur�e de l'animation de disparition/r�apparition
    public Vector3 deformationFactor = new Vector3(1.5f, 0.3f, 1f); // Facteur de d�formation (x, y, z)

    private SpriteRenderer spriteRenderer; // R�f�rence au SpriteRenderer du boss
    private Vector3 originalScale; // �chelle originale du boss
    private int currentPointIndex = 0; // Indice du point actuel de t�l�portation

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // R�cup�re le SpriteRenderer
        originalScale = transform.localScale; // Sauvegarde l'�chelle originale
        StartCoroutine(TeleportRoutine()); // Lance la routine de t�l�portation
    }

    IEnumerator TeleportRoutine()
    {
        while (true)
        {
            // Disparition et d�formation du boss (fade out + deformation)
            yield return StartCoroutine(FadeOutAndDeform());

            // T�l�portation vers le prochain point
            transform.position = teleportationPoints[currentPointIndex].position;

            // R�apparition du boss (fade in + restauration de la d�formation)
            yield return StartCoroutine(FadeInAndRestore());

            // Passer au point suivant
            currentPointIndex = (currentPointIndex + 1) % teleportationPoints.Length;

            // Attendre avant la prochaine t�l�portation
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
            Vector3 scale = Vector3.Lerp(initialScale, deformationFactor, elapsedTime / fadeDuration); // D�formation progressive

            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            transform.localScale = scale; // Applique la d�formation

            yield return null;
        }

        // Assurez-vous que l'alpha et l'�chelle soient � leurs valeurs finales
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        transform.localScale = deformationFactor; // Applique la d�formation finale
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
            transform.localScale = scale; // Restaure l'�chelle

            yield return null;
        }

        // Assurez-vous que l'alpha et l'�chelle soient � leurs valeurs finales
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
        transform.localScale = originalScale; // Restaure l'�chelle originale
    }
}
