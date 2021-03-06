﻿using System.Linq;
using TMPro;
using UnityEngine;



public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI[] textItems = new TextMeshProUGUI[3];
    public Transform leaderboardItem;
    public Canvas canvas;


    private TextMeshProUGUI[] leaderboardItems;



    private int currentSelectedItem;
    private bool keyFirstPressed;
    private bool keyReleased = true;



    void Start()
    {
        textItems[0].color = Color.gray;



        leaderboardItems = leaderboardItem.GetComponentsInChildren<TextMeshProUGUI>().Where(t => t.fontSize < 30).ToArray();
        LeaderboardManager.LoadLeaderboardEntries();



        for (int i = 9; i >= LeaderboardManager.leaderboardEntries.Count; i--)
            leaderboardItems[i].transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < 10 && i < LeaderboardManager.leaderboardEntries.Count; i++)
            leaderboardItems[i].text = LeaderboardManager.leaderboardEntries[i].ToString();
    }



    void Update()
    {
        int menuMove = -(int)(DungeonGameManager.GetMovementVector().y + DungeonGameManager.GetAimingVector().y);
        keyFirstPressed = menuMove != 0 && keyReleased;
        keyReleased = menuMove == 0;

        if (keyFirstPressed)
        {
            textItems[currentSelectedItem].color = Color.white;

            currentSelectedItem = (currentSelectedItem + menuMove) % textItems.Length;
            if (currentSelectedItem < 0)
                currentSelectedItem = textItems.Length - 1;

            textItems[currentSelectedItem].color = Color.gray;
        }



        if (Input.GetAxis("Fire1") > 0 || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            MenuSelect();
    }



    public void MenuSelect()
    {
        switch (currentSelectedItem)
        {
            // Start new game
            case 0:
                LevelManager.LoadStartingLevel();
                break;

            // Options
            case 1:
                break;

            // Exit Game
            case 2:
                DungeonGameManager.QuitApplication();
                break;
        }
    }
}
