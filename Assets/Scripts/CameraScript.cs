using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform sphare;
    [SerializeField]
    private Transform cameraFixed;

    private Vector3 rod;
    private bool isFixed;
    private InputAction lookAction;
    private InputAction zoomAction;
    private float rotV;
    private float rotH;
    private float sensV = 5f;
    private float sensH = 1f;
    private float minZoom = 25f;  // Минимальное расстояние
    private float maxZoom = 70f;  // Максимальное расстояние
    private float currentZoom;
    private float zoomSpeed = 2f;  // Скорость зума


    void Start()
    {
        isFixed = false;
        rod = this.transform.position - sphare.position;
        lookAction = InputSystem.actions.FindAction("Look");
        zoomAction = InputSystem.actions.FindAction("Zoom");
        rotV = this.transform.eulerAngles.x;
        rotH = this.transform.eulerAngles.y;
        currentZoom = rod.magnitude;
    }

    private void LateUpdate()
    {
        if (!isFixed)
        {
            Vector2 lookValue = lookAction.ReadValue<Vector2>();

            float dV = -lookValue.y * Time.deltaTime * sensV;
            if (rotV + dV >= 35 && rotV + dV <= 65)
            {
                rotV += dV;
            }

            rotH += lookValue.x * sensH;

            this.transform.eulerAngles = new Vector3(rotV, rotH, 0f);

            Vector2 zoomValue = zoomAction.ReadValue<Vector2>();
            float zoomChange = zoomValue.y * zoomSpeed; // Используем только вертикальный компонент

            currentZoom -= zoomChange;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            
            this.transform.position = sphare.position  + (Quaternion.Euler(0, rotH, 0) * rod.normalized)*currentZoom;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isFixed = !isFixed;
            if (isFixed)
            {
                this.transform.position = cameraFixed.position;
                this.transform.rotation = cameraFixed.rotation;
            }
        }
    }
}
