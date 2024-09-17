using UnityEngine;

public class InfectedNPC : MonoBehaviour
{
    public Color infectedColor = Color.magenta; // Color morado para el estado infectado
    public Color normalColor = Color.white;     // Color normal para el estado curado
    public float trembleIntensity = 0.1f;        // Intensidad del temblor
    public float trembleSpeed = 5f;             // Velocidad del temblor
    public float cureDuration = 3f;             // Duraci�n de la curaci�n
    public Rigidbody2D Rigidbody2D;

    private bool isInfected = true;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    private bool isTrembling = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        // Inicialmente el NPC est� infectado
        SetInfectedState();
    }

    void Update()
    {
        if (isTrembling)
        {
            Tremble();
        }
    }

    private void SetInfectedState()
    {
        spriteRenderer.color = infectedColor;
        isTrembling = true;
    }

    private void SetNormalState()
    {
        spriteRenderer.color = normalColor;
        isTrembling = false;
    }

    private void Tremble()
    {
        // Simula el temblor moviendo el NPC ligeramente
        float trembleAmount = Mathf.Sin(Time.time * trembleSpeed) * trembleIntensity;
        transform.position = originalPosition + new Vector3(trembleAmount, trembleAmount, 0f);
    }

    public void Cure()
    {
        if (isInfected)
        {
            isInfected = false;
            SetNormalState();
            Invoke("Reinfect", cureDuration); // Reinfecci�n despu�s de la duraci�n
        }
    }

    private void Reinfect()
    {
        // Reiniciar el estado infectado despu�s del tiempo de curaci�n
        isInfected = true;
        SetInfectedState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene el tag "Caut"
        if (collision.gameObject.CompareTag("Caut"))
        {
            Cure(); // Llama al m�todo de curaci�n
            Destroy(collision.gameObject); // Destruye el proyectil despu�s de la colisi�n
        }
        Debug.Log("No me toques");

    }

}
