using UnityEngine;
using System.IO.Ports;
using System.Globalization;
using System;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UISlot
{
    private Image _slot;

    public UISlot(Image slot)
    {
        slot = this._slot;
    }

    public bool CheckStatus()
    {
        if (_slot.sprite == null)
        {
            Debug.Log("Available");
            return true;
        }
        else
        {
            Debug.Log("Occupied");
            return false;
        }
    }

    public void AddIcon(Sprite icon)
    {
        _slot.sprite = icon;
    }

    public void RemoveIcon()
    {
        _slot.sprite = null;
    }
}

//Script meant to send a summon call to Nano then register the data.
public class AddMember : MonoBehaviour
{
    SerialPort port = new SerialPort("COM4", 9600);
    string summon = "SUMMON";
    string data;
    [SerializeField] CharacterDataBase figureBase;

    private static bool IsHexChar(char c) =>
    (c >= '0' && c <= '9') ||
    (c >= 'A' && c <= 'F') ||
    (c >= 'a' && c <= 'f');


    //FOR UI
    [SerializeField] Image icon1;
    [SerializeField] Image icon2;
    [SerializeField] Image icon3;

    private UISlot slot1;
    private UISlot slot2;
    private UISlot slot3;

    private UISlot[] characterSlots;

    void Awake()
    {
        slot1 = new UISlot(icon1);
        slot2 = new UISlot(icon2);
        slot3 = new UISlot(icon3);

        characterSlots = new UISlot[] { slot1, slot2, slot3 };
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        port.Open();
        port.ReadTimeout = 3000;
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
        if (port == null || !port.IsOpen)
        {
            Debug.Log("port not opened");
            return;
        }

        string line;
        try
        {
            line = port.ReadLine();
        }
        catch (TimeoutException)
        {
            Debug.LogWarning("Timed out waiting for Arduino line.");
            return;
        }

        line = line.Trim();
        Debug.Log($"Nano sent raw: '{line}'");

        // Handle error/status lines from Arduino
        if (line.StartsWith("NO_CARD") || line.StartsWith("READ_FAIL"))
        {
            Debug.LogWarning($"Arduino status: {line}");
            return;
        }

        string hex = line.Replace(" ", "").ToUpperInvariant();

        // Validate it looks like 32 hex chars (16 bytes)
        if (hex.Length != 32 || !hex.All(IsHexChar))
        {
            Debug.LogWarning($"Unexpected payload: '{hex}' (len {hex.Length})");
            return;
        }

        // First byte is the ID (02 for medic etc.)
        if (!int.TryParse(hex.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int receivedID))
        {
            Debug.LogWarning($"Could not parse ID from '{hex}'");
            return;
        }

        for (int i = 0; i < figureBase.database.Count; i++)
        {
            if (figureBase.database[i].CharacterID == receivedID)
            {
                Debug.Log($"Found character! Parsed ID: {receivedID}");
                CharacterData playable = figureBase.database[i];

                SetSlot(playable);

                return;
            }
            else
            {
                Debug.Log($"Character not found Parsed ID: {receivedID}");
            }
        }
    }

    public void SetSlot(CharacterData figure)
    {
        foreach (UISlot slot in characterSlots)
        {
            if (slot.CheckStatus())
            {
                Sprite characterIcon = figure.icon;
                slot.AddIcon(characterIcon);
                Debug.Log("Icon successfully added");
                return;
            }
            else
            {
                Debug.LogWarning("Icon spot taken cannot add icon!");
            }
        }
    }
}
