using UnityEngine;
using System.IO.Ports;
using System.Globalization;
using UnityEngine.TextCore.Text;
using UnityEditor.Experimental.GraphView;

//Script meant to send a summon call to Nano then register the data.
public class AddMember : MonoBehaviour
{
    SerialPort port = new SerialPort("COM4", 9600);
    string summon = "SUMMON";
    string data;
    [SerializeField] CharacterDataBase figureBase;


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
        int recievedID = int.Parse(data.Substring(0,2), NumberStyles.HexNumber);

        for (int i = 0; i > figureBase.database.Count - 1; i++)
        {
            if (figureBase.database[i].CharacterID == recievedID)
            {
                Debug.Log("Found character!");
                break;
            }
            else{ Debug.Log("Not found yet");}
        }
    }
}
