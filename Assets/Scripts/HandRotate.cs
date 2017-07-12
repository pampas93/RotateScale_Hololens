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

    public char rotateAxis = ' ';

    private GameObject child;
    //private xAxisRotate xObj;
    //private yAxisRotate yObj;
    //private zAxisRotate zObj;

    Vector3 lastRotation;

    public void Start()
    {
        Debug.Log("Application rotation script started");

        child = this.transform.GetChild(0).gameObject;

        //xObj = child.GetComponentInChildren<xAxisRotate>();
        //yObj = child.GetComponentInChildren<yAxisRotate>();
        //zObj = child.GetComponentInChildren<zAxisRotate>();

        //xObj.selected = false;
        //yObj.selected = false;
        //zObj.selected = false;
    }

    [SerializeField]
    bool rotatingEnabled = true;
    public void SetRotating(bool enabled)
    {
        rotatingEnabled = enabled;
    }

    private void Update()
    {
        if (rotatingEnabled)
            child.SetActive(true);
        else
            child.SetActive(false);
           

    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        //Debug.Log("Starting Manipulation");
        //Debug.Log(transform.rotation.x + "____" + transform.rotation.y + "_____" + transform.rotation.z);
        //Pushing this gameObject into the Modal Input Stack
        InputManager.Instance.PushModalInputHandler(gameObject);
        lastRotation = transform.rotation.eulerAngles;
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
        //Debug.Log("Completed Manipulation");
        //Debug.Log(transform.rotation.x + "____" + transform.rotation.y + "_____" + transform.rotation.z);
        InputManager.Instance.PopModalInputHandler();
        lastRotation = transform.rotation.eulerAngles;
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
                transform.rotation = Quaternion.Euler((lastRotation.x + rotation.x) * RotateSpeed, lastRotation.y,lastRotation.z);
                //transform.Rotate(Vector3.right * rotation.x);
                //xObj.selected = true;
                //yObj.selected = false;
                //zObj.selected = false;
                break;
            case 'Y':
                transform.rotation = Quaternion.Euler(lastRotation.x,( lastRotation.y + rotation.y)*RotateSpeed,lastRotation.z);
                //transform.Rotate(Vector3.down * rotation.y);
                //yObj.selected = true;
                //xObj.selected = false;
                //zObj.selected = false;
                break;
            case 'Z':
                transform.rotation = Quaternion.Euler(lastRotation.x, lastRotation.y, (lastRotation.z + rotation.z)*RotateSpeed) ;
                //transform.Rotate(Vector3.forward * rotation.z);
                //zObj.selected = true;
                //yObj.selected = false;
                //xObj.selected = false;
                break;
            default:
                //xObj.selected = false;
                //yObj.selected = false;
                //zObj.selected = false;
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
