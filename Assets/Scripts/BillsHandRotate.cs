using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class BillsHandRotate : MonoBehaviour, IManipulationHandler
{
    [Tooltip("Speed of static rotation when tapping game object.")]
    [SerializeField]
    float RotateSpeed = 25f;

    [Tooltip("Speed of interactive rotation via navigation gestures.")]
    [SerializeField]
    float RotationFactor = 50f;

    Quaternion lastRotation;

    [SerializeField]
    bool rotatingEnabled = true;
    public void SetRotating(bool enabled)
    {
        rotatingEnabled = enabled;
    }


    private void Update()
    {
        //transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        //transform.RotateAround(transform.position, transform.right, Time.deltaTime * 90f);
        var yAmount = 1.0f;

        //transform.Rotate(yAmount, yAmount, 0, Space.World);
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        //Pushing this gameObject into the Modal Input Stack
        InputManager.Instance.PushModalInputHandler(gameObject);
        lastRotation = transform.rotation; 
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (rotatingEnabled)
        {
            //var rotation = new Quaternion(eventData.CumulativeDelta.x * RotationFactor,
            //    eventData.CumulativeDelta.y * RotationFactor,
            //    eventData.CumulativeDelta.z * RotationFactor,
            //    0f);

            //Rotate(rotation);

            var rotation = new Vector3(eventData.CumulativeDelta.y * RotationFactor,
                    eventData.CumulativeDelta.x * RotationFactor,
                    eventData.CumulativeDelta.z * RotationFactor);

            Rotate2(rotation);



            //sharing & messaging
            //SharingMessages.Instance.SendRotating(Id, eventData.CumulativeDelta);
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    void Rotate(Quaternion rotation)
    {
        transform.rotation = Quaternion.Euler(
            new Vector3(lastRotation.x + rotation.x,
                 lastRotation.y + rotation.y,
                 lastRotation.z + rotation.z) * RotateSpeed);
    }

    void Rotate2(Vector3 rotation)
    {
        //transform.rotation = Quaternion.Euler(
        //    new Vector3(lastRotation.x + rotation.x,
        //         lastRotation.y + rotation.y,
        //         lastRotation.z + rotation.z) * RotateSpeed);

        var x = rotation.x * RotateSpeed;
        var y = rotation.y * RotateSpeed;
        var z = rotation.z * RotateSpeed;
        transform.Rotate(x, y, z, Space.Self);
    }
}
