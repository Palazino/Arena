using UnityEngine;

public class BossRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; // Vitesse de rotation

    private void Update()
    {
        // Appliquer la rotation
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
