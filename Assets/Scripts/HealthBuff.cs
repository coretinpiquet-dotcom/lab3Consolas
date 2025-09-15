using Tanks.Complete;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{

    public int BuffValue = 10;
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SimplePlayer>().ModifyLife(BuffValue);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
