using UnityEngine;
using System.IO.Ports;

//Script meant to send a summon call to Nano then register the data.
public class AddMember : MonoBehaviour
{
    SerialPort port = new SerialPort("COM4", 9600);
    string summon = "SUMMON";
    string data;
    [SerializeField] CharacterDataBase dataBase;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        port.Open();
        port.ReadTimeout = 500;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SummonRequest()
    {
        if (!port.IsOpen)
        {
            Debug.Log("port not opened");
            return;
        }

        port.WriteLine(summon);
    }

    public void RegisterCharacter()
    {
        if (!port.IsOpen)
        {
            Debug.Log("port not opened");
            return;
        }

        data = port.ReadLine();
        Debug.Log($"Nano sent: {data}");
        
    }


}
