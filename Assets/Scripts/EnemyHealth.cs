using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Vector3 sliderOffset = new Vector3(0, 2f, 0);

    private float currentHealth;
    private Camera mainCamera;

    void Start()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main;

        if (healthSlider == null)
        {
            Debug.LogWarning("⚠️ No se asignó un Slider de vida al enemigo " + gameObject.name);
        }
        else
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        if (healthSlider != null)
        {
            // Si el slider está en un Canvas en World Space
            healthSlider.transform.position = transform.position + sliderOffset;

            // Para que siempre mire a la cámara
            healthSlider.transform.rotation = mainCamera.transform.rotation;

            // Si usas Canvas en Screen Space:
            // healthSlider.transform.position = mainCamera.WorldToScreenPoint(transform.position + sliderOffset);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (healthSlider != null)
        {
            Destroy(healthSlider.gameObject);
        }

        Destroy(gameObject);
    }
}
