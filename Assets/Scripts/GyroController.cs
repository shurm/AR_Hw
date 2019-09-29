using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GyroController : MonoBehaviour
{
    public Text debugText;
    public GameObject ControlledObject
    {
        get { return controlledObject; }
        set
        {
            controlledObject = value;
            ResetOrientation();
        }
    }


    public bool Paused { get; set; }

    Quaternion qRefObject = Quaternion.identity;
    Quaternion qRefGyro = Quaternion.identity;
	Quaternion qRefGyroLeft = Quaternion.identity;
    Gyroscope gyro;

    GameObject controlledObject;

    private Quaternion rot = Quaternion.identity;
    private void Awake()
    {
        Paused = false;
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        gyro = Input.gyro;
        gyro.enabled = true;
        gyro.updateInterval = 0.01f;
    }

    private void OnGUI()
    {
        GUILayout.Label("Gyroscope attitude : " + gyro.attitude);
        GUILayout.Label("Gyroscope attitude : " + gyro.attitude.eulerAngles);
        GUILayout.Label("Gyroscope gravity : " + gyro.gravity);
       
    }

    // LOOK-1.d:
    // Converts the data returned from gyro from right-handed base to left-handed base.
    // Your device may require a different conversion
    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private void Update()
    {
        if (controlledObject != null && !Paused)
        {
            // TODO-1.d & TODO-2.a:
            //   rotate the camera or cube based on qRefObject, qRefGyro and current 
            //   data from gyroscope
            // UpdateOrientation(Time.deltaTime);
            //controlledObject.transform.rotation = ConvertRotation(gyro.attitude)*rot;
            //[phone's orientation this frame] = [starting orientation of the cube GameObject] * Quaternion.Inverse([starting orientation of the physical phone]) * [current orientation of the physical phone]
            controlledObject.transform.rotation = qRefObject*Quaternion.Inverse(qRefGyro) * ConvertRotation(gyro.attitude);
    debugText.text = controlledObject.transform.rotation.ToString();
        }
    }

    public void ResetOrientation()
    {
        if (controlledObject == null)
        {
            return;
        }
        qRefObject = controlledObject.transform.rotation;
        qRefGyro = ConvertRotation(Input.gyro.attitude);
		qRefGyroLeft = Input.gyro.attitude;
    }

     //Possible helper function to smooth between gyro and Vuforia
     public void UpdateOrientation(float deltatime)
     {
             float smooth = 1f;
        //         qRefCam = Quaternion.Slerp(qRefCam, transform.rotation, smooth * deltatime);
        //qRefObject = Quaternion.Slerp(qRefObject, ConvertRotation(gyro.attitude), smooth * deltatime);
      //  ControlledObject.transform.rotation = qRefObject;
     }
}
