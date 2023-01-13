using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lost : MonoBehaviour
{
    public GameObject player;
    public GameObject LostMenuUI;
    public static bool lost = false;

    void Start()
    {
        lost = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player.GetComponent<DeathController>().killed)
        {
            lost = true;
            Time.timeScale = 0f;
            LostMenuUI.SetActive(true);
        }
    }

    public void OnStartGameAgainClick()
    {
        MenuController.Instance.StartGameAgain();
    }

    public void OnQuitClick()
    {
        MenuController.Instance.QuitGame();
    }
}
