using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject barObject;

    private float baseHealth = 100f;
    private float currentHealth = 100f;
    private float initialYScale;

    private void Awake()
    {
        if (barObject != null)
        {
            initialYScale = barObject.transform.localScale.y;
        }
    }

    public void SetBaseHealth(float health)
    {
        baseHealth = Mathf.Max(health, 0f);
        currentHealth = baseHealth;
        UpdateScale();
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0f, baseHealth);
        UpdateScale();
    }

    private void UpdateScale()
    {
        if (barObject == null) return;

        Vector3 scale = barObject.transform.localScale;
        scale.y = (baseHealth > 0f) ? initialYScale * (currentHealth / baseHealth) : 0f;
        barObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
    }
}