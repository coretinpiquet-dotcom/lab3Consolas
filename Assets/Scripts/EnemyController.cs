using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject _enemyShell;
    public Transform _firePoint;
    public float _shootingRate;
    public float _shootingRange;
    public float _rotationSpeed;
    public int _life = 10;

    private Transform _target = null;
    private float _currentTimeSpanned = 0f;

    void Start() {
        
    }

    void Update()
    {
        if (_life <= 0)
            Destroy(gameObject);

        _currentTimeSpanned += Time.deltaTime;
        if (_target == null) {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                _target = playerObj.transform;
        }
        if (_target == null)
            return;

        if (Vector3.Distance(transform.position, _target.position) > _shootingRange)
            return;

        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        if (_currentTimeSpanned > _shootingRate) {
            print("Enemy Shoot");
            Instantiate(_enemyShell, _firePoint.position, _firePoint.rotation);
            _currentTimeSpanned = 0;
        }
    }

    public void ModifyLife(int delta) {
        _life += delta;
    }
}
