using UnityEngine;

public class BatteryControll : MonoBehaviour
{
    public float rotationSpeed = 50f; 
    public float bounceAmplitude = 0.5f; 
    public float bounceFrequency = 2f; 

    private Vector3 startPosition; 

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
