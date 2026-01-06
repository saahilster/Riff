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
    private Transform _pos;
    public UISlot(Image slot)
    {
        this._slot = slot;
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
    //DW about removing character
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
    private Transform leader;
    [SerializeField] CharacterDataBase figureBase;
    [SerializeField] CharacterDataBase partyBase;

    private static bool IsHexChar(char c) =>
    (c >= '0' && c <= '9') ||
    (c >= 'A' && c <= 'F') ||
    (c >= 'a' && c <= 'f');

    [SerializeField] ActivePartyManager partyManager;

    //UI   
    [SerializeField] Image icon1;
    [SerializeField] Image icon2;
    [SerializeField] Image icon3;

    private UISlot slot1;
    private UISlot slot2;
    private UISlot slot3;
    private UISlot[] characterSlots;

    //Transform references for party
    [SerializeField] Transform leaderPosition;
    [SerializeField] Transform follower1;
    [SerializeField] Transform follower2;
    private Transform[] positions;

    void Awake()
    {
        if (partyBase != null && partyBase.database != null)
        {
            partyBase.database.Clear();
        }
        slot1 = new UISlot(icon1);
        slot2 = new UISlot(icon2);
        slot3 = new UISlot(icon3);

        characterSlots = new UISlot[] { slot1, slot2, slot3 };
        // positions = new Transform[] { leader, follower1, follower2 };
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
                CheckMember(playable);
                Debug.Log("Finished adding to the party");
                return;
            }
            else
            {
                Debug.Log($"Character not found Parsed ID: {receivedID}");
            }
        }
    }

    //gets called in Register Chracter
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
    //Iterates through current party to make sure there isn't duplicates
    public void CheckMember(CharacterData figure)
    {

        if (partyBase.database.Count >= 3)
        {
            Debug.Log("Party full cannot add anymore.");
        }
        if (partyBase.database == null)
        {
            Debug.Log("Null base");
        }
        if (partyBase.database.Contains(figure))
        {
            Debug.Log("figure already in party");
            return;
        }

        SetSlot(figure);
        partyBase.database.Add(figure);

        if (figure.partyPrefab.GetComponent<PlayerMovement>() != null)
        {
            GameObject member = Instantiate(figure.partyPrefab, leaderPosition);
            leader = member.transform;
        }
        else
        {
            GameObject member = Instantiate(figure.partyPrefab);
            member.GetComponent<PlayerFollow>().SetTarget(leader);
        }
        partyManager.currentSize++;
    }

    public static void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}