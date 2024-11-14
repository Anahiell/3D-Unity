using UnityEngine;
using UnityEngine.InputSystem;

public class SphereScript : MonoBehaviour
{
    [SerializeField]
    private float forceFactor = 10f;
    private InputAction moveAction;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Camera.main.transform.forward;

        right.y = 0f;
        forward.y = 0f;

        right.Normalize();
        forward.Normalize();

        Vector3 force = right * moveValue.x * forceFactor + forward * moveValue.y * forceFactor;
        rb.AddForce(force);
    }
}
