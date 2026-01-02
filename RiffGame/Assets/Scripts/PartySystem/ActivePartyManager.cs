using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum EditStatus
{
    Idle,
    Adding,
    Deleting
}

public class ActivePartyManager : MonoBehaviour
{
    public UnityEvent AddingMember;
    public UnityEvent RemovingMember;

    //Might not need status enum.
    EditStatus status;
    public int maxCount = 3;
    public int currentSize = 0;
    [SerializeField] CharacterDataBase party;
    [SerializeField] CharacterDataBase dataBase;

    //noti is the text to display current status of party editing.
    [SerializeField] TextMeshProUGUI noti;
    private string[] notiCodes = {
        "Adding Member - Place them on scanner.",
        "Removing Member - Place them on.",
        "Error: Party already full CANNOT add more",
        "Error: Party empty CANNOT remove any more",
        "Select Add or Remove"
        };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        status = EditStatus.Idle;
        Debug.Log(party.database.Count);
        ResetNoti();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetNoti()
    {
        noti.text = notiCodes[4];
    }

    public void AddMode()
    {
        if (currentSize >= maxCount)
        {
            noti.text = notiCodes[2];
            status = EditStatus.Idle;
            return;
        }

        status = EditStatus.Adding;
        noti.text = notiCodes[0];
    }

    public void DeleteMode()
    {
        if (currentSize <= 0)
        {
            noti.text = notiCodes[3];
            status = EditStatus.Idle;
            return;
        }

        status = EditStatus.Deleting;
        noti.text = notiCodes[1];
    }

}
