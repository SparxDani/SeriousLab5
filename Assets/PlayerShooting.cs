using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject basicShotPrefab;       // Prefab del disparo básico
    public GameObject cauterizerShotPrefab;  // Prefab del disparo Cauterizador
    public Transform shotSpawnPoint;         // Punto desde donde se disparan los proyectiles
    public TMP_Text cauterizerShotsText;         // Texto que muestra la cantidad de disparos de Cauterizador
    public int maxCauterizerShots = 10;      // Máximo de disparos de Cauterizador

    private int currentCauterizerShots;
    private bool isCauterizerMode = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentCauterizerShots = maxCauterizerShots;
        UpdateCauterizerText();
        StartCoroutine(RegenerateCauterizerShots());
    }

    void Update()
    {
        HandleShooting();
        HandleCauterizerMode();
    }

    void HandleShooting()
    {
        // Disparar cuando se presiona la tecla de disparo (por ejemplo, la tecla "Z")
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isCauterizerMode && currentCauterizerShots > 0)
            {
                ShootCauterizer();
            }
            else
            {
                ShootBasic();
            }
        }
    }

    void HandleCauterizerMode()
    {
        // Cambiar al modo Cauterizador cuando se presiona la tecla Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isCauterizerMode = !isCauterizerMode;
        }
    }

    void ShootBasic()
    {
        GameObject shot = Instantiate(basicShotPrefab, shotSpawnPoint.position, Quaternion.identity);
        shot.GetComponent<Projectile>().SetDirection(spriteRenderer.flipX ? Vector2.left : Vector2.right);
    }

    void ShootCauterizer()
    {
        GameObject shot = Instantiate(cauterizerShotPrefab, shotSpawnPoint.position, Quaternion.identity);
        shot.GetComponent<Projectile>().SetDirection(spriteRenderer.flipX ? Vector2.left : Vector2.right);
        currentCauterizerShots--;
        UpdateCauterizerText();
    }

    void UpdateCauterizerText()
    {
        cauterizerShotsText.text = $"Cauterizer Shots: {currentCauterizerShots}";
    }

    IEnumerator RegenerateCauterizerShots()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (currentCauterizerShots < maxCauterizerShots)
            {
                currentCauterizerShots++;
                UpdateCauterizerText();
            }
        }
    }

    public void ReloadCauterizerShots(int amount)
    {
        currentCauterizerShots = Mathf.Clamp(currentCauterizerShots + amount, 0, maxCauterizerShots);
        UpdateCauterizerText();
    }
}