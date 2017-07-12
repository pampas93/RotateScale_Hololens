using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class HandRotate : MonoBehaviour, IManipulationHandler
{
    [Tooltip("Speed of static rotation when tapping game object.")]
    [SerializeField]
    float RotateSpeed = 25f;

    [Tooltip("Speed of interactive rotation via navigation gestures.")]
    [SerializeField]
    float RotationFactor = 20f;

    public char rotateAxis = 'Y';

    Quaternion lastRotation;

    public void Start()
    {
        Debug.Log("Application rotation script started");
    }

    [SerializeField]
    bool rotatingEnabled = true;
    public void SetRotating(bool enabled)
    {
        rotatingEnabled = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        Debug.Log("Starting Manipulation");
        Debug.Log(transform.rotation.x + "____" + transform.rotation.y + "_____" + transform.rotation.z);
        //Pushing this gameObject into the Modal Input Stack
        InputManager.Instance.PushModalInputHandler(gameObject);
        //lastRotation = transform.rotation;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        //Debug.Log("Updating Manipulation");
        if (rotatingEnabled)
        {
            var rotation = new Vector3(eventData.CumulativeDelta.y * RotationFactor, 
                eventData.CumulativeDelta.x * RotationFactor, 
                eventData.CumulativeDelta.z * RotationFactor);

            Rotate(rotation);

            //sharing & messaging
            //SharingMessages.Instance.SendRotating(Id, eventData.CumulativeDelta);
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        Debug.Log("Completed Manipulation");
        Debug.Log(transform.rotation.x + "____" + transform.rotation.y + "_____" + transform.rotation.z);
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    void Rotate(Vector3 rotation)
    {
        //Debug.Log(" X = " + rotation.x + "... Y = " + rotation.y + "... Z = " + rotation.z);
        switch (rotateAxis)
        {
            case 'X':
                transform.Rotate(Vector3.right * rotation.x);
                break;
            case 'Y': transform.Rotate(Vector3.down * rotation.y);
                break;
            case 'Z': transform.Rotate(Vector3.forward * rotation.z);
                break;
        }
    }
    /*void Rotate(Quaternion rotation)
    {
        //Debug.Log(" X = " + rotation.x + "... Y = " + rotation.y + "... Z = " + rotation.z);
        switch (rotateAxis)
        {
            case 'X':
                transform.rotation = Quaternion.Euler(
                    new Vector3(lastRotation.x + rotation.x,
                        lastRotation.y,
                        lastRotation.z) * RotateSpeed);
                break;
            case 'Y':
                transform.rotation = Quaternion.Euler(
                    new Vector3(lastRotation.x,
                        lastRotation.y + rotation.y,
                        lastRotation.z) * RotateSpeed);
                break;
            case 'Z':
                transform.rotation = Quaternion.Euler(
                    new Vector3(lastRotation.x,
                        lastRotation.y,
                        lastRotation.z + rotation.z) * RotateSpeed);
                break;

        }
    }*/
}
