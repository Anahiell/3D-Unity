using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform sphare;
    [SerializeField]
    private Transform cameraFixed;
    [SerializeField]
    private float minFpvDistance = 1.0f;
    [SerializeField]
    private float maxCameraDistance = 10.0f;

    private Vector3 rod;
    private bool isFixed;

    private InputAction lookAction;

    private float rotV;
    private float rotH;

    private float sensV = 5f;
    private float sensH = 25f;

    private float minEulerAngel = 35f;
    bool isFpv = false;

    //private float minZoom = 25f;  // Минимальное расстояние
    //private float maxZoom = 70f;  // Максимальное расстояние
    //private float currentZoom;
    //private float zoomSpeed = 2f;  // Скорость зума


    void Start()
    {
        isFixed = false;
        rod = this.transform.position - sphare.position;
        lookAction = InputSystem.actions.FindAction("Look");
        rotV = this.transform.eulerAngles.x;
        rotH = this.transform.eulerAngles.y;
        //currentZoom = rod.magnitude;
    }

    private void LateUpdate()
    {
        if (!isFixed)
        {
            Vector2 zoom = Input.mouseScrollDelta;
            if (zoom.y != 0.0f)
            {
                if (rod.magnitude >= minFpvDistance)
                {
                    float k = 1 - zoom.y * 0.1f;
                    if ((rod * k).magnitude > minFpvDistance)
                    {
                        rod = rod * 0.5f; //
                        isFpv = true;
                    }
                    else if ((rod*k).magnitude <= maxCameraDistance)
                    {
                        rod = rod * k;
                    }
                }
                else
                {
                    if (zoom.y < 0.0f)
                    {
                        rod = rod * 101f;
                        isFpv = false;
                        if(this.transform.eulerAngles.x<minEulerAngel)
                        {
                            rotV =minEulerAngel;
                        }
                    }
                }
            }

            Vector2 lookValue = lookAction.ReadValue<Vector2>();

            float dV = -lookValue.y * Time.deltaTime * sensV;
            if (rotV + dV >= (isFpv ? 0 : minEulerAngel) && rotV + dV <= 65)
            {
                rotV += dV;
            }

            rotH += lookValue.x *Time.deltaTime* sensH;
            this.transform.eulerAngles = new Vector3(rotV, rotH, 0f);

            //Vector2 zoomValue = zoomAction.ReadValue<Vector2>();
            //float zoomChange = zoomValue.y * zoomSpeed; // Используем только вертикальный компонент

            //currentZoom -= zoomChange;
            //currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);


            this.transform.position = sphare.position + (Quaternion.Euler(0, rotH, 0) * rod)/**currentZoom*/;
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
