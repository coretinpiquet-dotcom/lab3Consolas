using UnityEngine;

public class MissileManager : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private ParticleSystem _explosionEffect;

    private void Update()
    {
        MoveMissile();
        CheckLifetime();
    }

    private void MoveMissile()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
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
        if (other.CompareTag("Enemy"))
        {
            Explode();
            Destroy(other.gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (_explosionEffect != null)
        {
            var explosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            explosion.Play();
        }
        Destroy(gameObject);
    }
}