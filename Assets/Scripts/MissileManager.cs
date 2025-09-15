using UnityEngine;

public class MissileManager : MonoBehaviour
{
    [SerializeField] private float _impulseForce = 20f;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private int _missileDamage = 10;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * _impulseForce, ForceMode.Impulse);
    }

    private void Update()
    {
        CheckLifetime();

        if (rb.linearVelocity.sqrMagnitude > 0.01f) {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
        }
    }

    private void CheckLifetime()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0f)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_lifetime > 4.9f)
            return;
        if (other.CompareTag("Enemy"))
        {
            Explode();
            print("Enemy got Shot by player");
            other.gameObject.GetComponent<EnemyController>().ModifyLife(-1 * _missileDamage);
        }
        else if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SimplePlayer>().ModifyLife(-1 * _missileDamage);
            Explode();
        }
        else if (!other.CompareTag("Missile"))
        {
            Explode();
            if (!other.CompareTag("Non-Destructible")) {
                Destroy(other.gameObject);
            }
        }
    }

    private void Explode()
    {
        if (_explosionEffect != null)
        {
            var explosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration);
        }
        Destroy(gameObject);
    }
}
