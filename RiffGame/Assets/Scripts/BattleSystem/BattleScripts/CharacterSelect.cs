using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


//Script will be used to choose a player then it will bring up the appropriate UI needed.
public class CharacterSelect : MonoBehaviour
{
    TurnManager tracker = TurnManager.instance;
    public CharacterClass selectedPlayer;
    public LayerMask mask;
    [SerializeField] GameObject ui;
    [SerializeField] Image[] skillSlots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.Escape))
        // {
        //     ui.SetActive(false);
        // }
        SelectPlayer();
    }

    private void SelectPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, mask))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    GameObject player = hit.collider.gameObject;
                    CharacterClass playerData = player.GetComponent<CharacterClass>();

                    if (playerData == null)
                    {
                        Debug.Log("Character Class not found");
                        return;
                    }
                    selectedPlayer = playerData;
                    SetUI(playerData, hit.collider.gameObject);
                }
            }
        }
    }

    private void SetUI(CharacterClass data, GameObject player)
    {
        for (int i = 0; i < 4; i++)
        {
            skillSlots[i].sprite = data.moveset[i].icon;
            MovesetTemplate moveScript = skillSlots[i].GetComponent<MovesetTemplate>();
            if (moveScript == null)
            {
                Debug.Log("moveset temp not found");
                return;
            }
            moveScript.battler = player;
            moveScript.moveData = data.moveset[i];

        }
        ui.SetActive(true);
    }
}
