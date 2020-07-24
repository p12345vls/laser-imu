using UnityEngine;
using Uduino;
using System.IO.Ports;

public class ReceiveIMUValues : MonoBehaviour {
    //SerialPort stream2 = new SerialPort("/dev/tty.usbmodem14301", 115200);

    Vector3 position;
    Vector3 rotation;
    public Vector3 rotationOffset ;
    public float speedFactor = 15.0f;
    public string imuName = "r"; // You should ignore this if there is one IMU.

    //public string receivedLaser;


    void Start () {
        //  UduinoManager.Instance.OnDataReceived += ReadIMU;
        //  Note that here, we don't use the delegate but the Events,
        // assigned in the Inpsector Panel
        //stream2.Open();
    }

    void Update()
    {
        float x = transform.Find("IMU_Object").eulerAngles.x;
        float y = transform.Find("IMU_Object").eulerAngles.y;
        float z = transform.Find("IMU_Object").eulerAngles.z;

        Camera.main.transform.localRotation = Quaternion.Euler(x, y, z);
        //Debug.Log("The rotation is: " + transform.Find("IMU_Object").eulerAngles);


        //receivedLaser = stream2.ReadLine();
        //stream2.BaseStream.Flush(); //Clear the serial information so we assure we get new information.


  

        //Debug.Log(receivedLaser);
    }


    public void ReadIMU (string data, UduinoDevice device) {
        //Debug.Log(data);
        string[] values = data.Split('/');
        if (values.Length == 5 && values[0] == imuName) // Rotation of the first one 
        {
            float w = float.Parse(values[1]);
            float x = float.Parse(values[2]);
            float y = float.Parse(values[3]);
            float z = float.Parse(values[4]);
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation,  new Quaternion(w, y, x, z), Time.deltaTime * speedFactor);
        } else if (values.Length != 5)
        {
            Debug.LogWarning(data);
        }
        this.transform.parent.transform.eulerAngles = rotationOffset;
      //  Log.Debug("The new rotation is : " + transform.Find("IMU_Object").eulerAngles);
    }
}
