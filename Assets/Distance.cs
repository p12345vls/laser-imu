using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;




public class Distance : MonoBehaviour
{
    SerialPort stream2 = new SerialPort("/dev/tty.usbmodem14301", 115200);
    public string receivedLaser;

    

    // Start is called before the first frame update
    void Start()
    {
        stream2.Open();

    }

    // Update is called once per frame
    void Update()
    {

        receivedLaser = stream2.ReadLine();
        stream2.BaseStream.Flush(); //Clear the serial information so we assure we get new information.

        

        transform.position = Vector3.Lerp(transform.position,
                 new Vector3(0, 0, (float.Parse(receivedLaser) / 10)-1.0f ), 5.0f * Time.deltaTime);

    }
}
