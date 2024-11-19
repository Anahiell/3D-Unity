using UnityEngine;
using UnityEngine.InputSystem;

public class SphereScript : MonoBehaviour
{
    [SerializeField]
    private float forceFactor = 10f;
    [SerializeField]
    private Light spotLight;

    private InputAction moveAction;
    private Rigidbody rb;
    private float charge = 100.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0.0f;
        camForward.Normalize();

        Vector3 right = Camera.main.transform.right;

        right.y = 0f;

        right.Normalize();

        Vector3 force = right * moveValue.x * forceFactor + camForward * moveValue.y * forceFactor;
        rb.AddForce(force);
        charge-=Time.deltaTime;
        if (charge < 0)
        {
            charge = 0;
        }
        spotLight.intensity = charge / 100.0f;
       spotLight.transform.forward = Camera.main.transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform.parent;
        if (t == null)
        {
            t = other.transform;
        }
        if(t.gameObject.CompareTag("Battery"))
        {
            charge = 100.0f;
        }
    }
}
