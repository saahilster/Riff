using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UIMain : MonoBehaviour
{
    [SerializeField] GameObject partyUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject hud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        partyUI.SetActive(false);
        menuUI.SetActive(false);
        hud.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableMenu()
    {
        hud.SetActive(false);
        menuUI.SetActive(true);
    }

    public void DisableMenu()
    {
        menuUI.SetActive(false);
        hud.SetActive(true);
    }

    public void EnablePartyUI()
    {
        menuUI.SetActive(false);
        partyUI.SetActive(true);
    }

    public void ReturnToMenu()
    {
        partyUI.SetActive(false);
        menuUI.SetActive(true);
    }
}
