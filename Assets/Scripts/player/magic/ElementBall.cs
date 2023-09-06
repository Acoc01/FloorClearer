using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBall : MonoBehaviour
{
    public Camera caster;
    public float ballNumber;
    public Element elementType;
    [SerializeField] private float _spacing = 0.2f;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;

    private float _zOffset = 0.4f;
    private float _xOffset = .5f;

    public float lerpSpeed = 5f;
    public float floatAmplitude = 0.05f;
    public float floatFrequency = 1f;

    private float phaseShift;

    private ParticleSystem particleSystem; // Reference to the Particle System

    private void Start()
    {
        _initialPosition = caster.transform.position;
        _targetPosition = _initialPosition + Vector3.right * (_spacing * ballNumber);

        // Calculate a unique phase shift based on the ball's position
        phaseShift = transform.position.x * 0.1f; // Adjust this factor to control the phase shift

        // Get the Particle System component attached to the ball, if available
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        // Calculate the camera's forward and right vectors
        Vector3 cameraForward = caster.transform.forward;
        Vector3 cameraRight = caster.transform.right;

        // Calculate the initial position for the first ball (top left corner)
        Vector3 initialPosition = caster.transform.position + cameraRight * (_spacing * (ballNumber - 1));

        Vector3 cameraUp = caster.transform.up;
        // Calculate the target position for the current ball (top right corner)
        Vector3 targetPosition = initialPosition + cameraForward + cameraUp * _zOffset;

        // Calculate the vertical oscillation based on time and phase shift
        float oscillation = Mathf.Sin((Time.time + phaseShift) * floatFrequency) * floatAmplitude;
        targetPosition += cameraUp * oscillation;

        // Interpolate the position of the elemental ball towards the target position
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.fixedDeltaTime);

        // Update the position of the elemental ball
        transform.position = newPosition;

        // Rotate the ball to face the camera
        Quaternion newRotation = Quaternion.LookRotation(caster.transform.forward, Vector3.up);
        transform.rotation = newRotation;

        // Check if Particle System is available and set its position
        if (particleSystem != null)
        {
            particleSystem.transform.position = transform.position;
        }
    }
}
