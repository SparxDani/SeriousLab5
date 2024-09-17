using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;
    private float lifetime = 3f; // Tiempo en segundos antes de destruir el proyectil

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    void Start()
    {
        // Inicia la coroutine para destruir el proyectil después de un tiempo
        //StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private IEnumerator DestroyAfterTime()
    {
        // Espera el tiempo especificado antes de destruir el proyectil
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
