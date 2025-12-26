using UnityEngine;
using System.IO.Ports;
public class ArduinoTestConnnect : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM3", 9600);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                Debug.Log("Arduino says: " + data);
            }
            catch { }
        }
    }
}
