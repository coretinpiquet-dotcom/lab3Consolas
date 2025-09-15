using System.Collections;
using Tanks.Complete;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{

    public int BuffValue = 10;
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SimplePlayer>().speed += BuffValue;
            other.gameObject.GetComponent<SimplePlayer>().rotationSpeed += BuffValue;
            other.gameObject.GetComponent<SimplePlayer>().tourelleRotationSpeed += BuffValue;
            StartCoroutine(BuffDuration(10, other.gameObject));
            transform.localScale = Vector3.zero;
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    IEnumerator BuffDuration(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.GetComponent<SimplePlayer>().speed -= BuffValue;
        obj.GetComponent<SimplePlayer>().rotationSpeed -= BuffValue;
        obj.GetComponent<SimplePlayer>().tourelleRotationSpeed -= BuffValue;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
