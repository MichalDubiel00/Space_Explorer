using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SpaceEnvironmentController : MonoBehaviour
{
    [SerializeField] XRJoystick thrustJoystick, verticalJoystick, rotationJoystick;

    public float rotationSpeed = 100f; 
    public float flightSpeed = 100f;  
    public float stopDamping = 2f;   

    [SerializeField] Transform environment; 

    void Start()
    {
    }

    void FixedUpdate()
    {      
        HandleShipRotation();
       
        HandleEnvironmentMovement();
    }

    void HandleShipRotation()
    {
        
        float yawInput = rotationJoystick.value.x;  
        float rollInput = rotationJoystick.value.y; 
 
        Quaternion yawRotation = Quaternion.Euler(0, yawInput * rotationSpeed * Time.fixedDeltaTime, 0); 
        Quaternion rollRotation = Quaternion.Euler(0, 0, -rollInput * rotationSpeed * Time.fixedDeltaTime); 
        
        transform.rotation *= yawRotation * rollRotation;
    }

    void HandleEnvironmentMovement()
    {        
        float forwardThrust = thrustJoystick.value.x; 
        float lateralThrust = thrustJoystick.value.y; 

       
        float verticalThrust = verticalJoystick.value.y;
       
        Vector3 movement = (-transform.forward * forwardThrust
                            + transform.right * lateralThrust
                            + transform.up * verticalThrust)
                            * flightSpeed * Time.fixedDeltaTime;
      
        environment.position += movement;
      
        if (Mathf.Abs(forwardThrust) < 0.01f && Mathf.Abs(lateralThrust) < 0.01f && Mathf.Abs(verticalThrust) < 0.01f)
        {
            environment.position = Vector3.Lerp(environment.position, environment.position + movement, Time.fixedDeltaTime * stopDamping);
        }
    }


    float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
