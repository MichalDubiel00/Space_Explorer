using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField] XRJoystick joystick1,joystick2,joystick3;

    Vector3 thrust;
    public float rotationSpeed = 100f; // Speed of rotation

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitRigidbody();
    }

    // Update is called once per frame
    void Update()
    {
        thrust.z = joystick1.value.x;
        thrust.y = joystick1.value.y;
        thrust.x = joystick2.value.x;

   

        rb.velocity = thrust;
    }
    private void FixedUpdate()
    {
        float yawInput = joystick3.value.x;
        float pitchInput = joystick3.value.y;

        // Calculate desired rotation
        Quaternion currentRotation = rb.rotation;
        Quaternion yawRotation = Quaternion.Euler(0, yawInput * rotationSpeed * Time.fixedDeltaTime, 0); // Yaw
        Quaternion pitchRotation = Quaternion.Euler(-pitchInput * rotationSpeed * Time.fixedDeltaTime, 0, 0); // Pitch

        // Combine rotations
        Quaternion targetRotation = currentRotation * yawRotation * pitchRotation;

        // Apply rotation using MoveRotation
        rb.MoveRotation(targetRotation);
    }

    void InitRigidbody()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.centerOfMass = Vector3.zero;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

}
