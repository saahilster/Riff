using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActionPointHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI apDisplay;
    public int actionPoints = 1;
    public float APMulti = 1;
    public float playerAP;
    public bool addedPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        addedPoints = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AddPoints());
        apDisplay.text = playerAP.ToString();
    }

    IEnumerator AddPoints()
    {
        if (!addedPoints)
        {
            addedPoints = true;
            playerAP = playerAP + (actionPoints * APMulti);
            yield return new WaitForSeconds(1);
            addedPoints = false;
        }
        
    }

}
