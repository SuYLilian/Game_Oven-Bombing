using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoddingArrow : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of rotation
    public float rotationSpeed_Run;
    private int rotationDirection = 1; // 1 for clockwise, -1 for counterclockwise

    public float maxRotationAngle = 68;

    private void Start()
    {
        rotationSpeed_Run = rotationSpeed;
    }
    void Update()
    {
        // Rotate the image
        RectTransform rectTransform = GetComponent<RectTransform>();
        float rotationAmount = rotationDirection * rotationSpeed_Run * Time.deltaTime;
        rectTransform.Rotate(Vector3.forward, rotationAmount);

        // Get the current rotation
        float currentRotation = rectTransform.localEulerAngles.z;
        if (currentRotation > 180)
        {
            currentRotation -= 360; // Convert to -180 to 180 range
        }

        // Adjust rotation direction if reaching angle limits
        if (currentRotation >= maxRotationAngle || currentRotation <= -maxRotationAngle)
        {
            rotationDirection *= -1; // Reverse rotation direction
        }
    }
}
