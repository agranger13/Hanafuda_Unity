using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnterGameUIController : MonoBehaviour
{
    public TextMeshProUGUI player0Name, player1Name, player0InGameName, player1InGameName;
    public Button start;
    public TMP_InputField player0NameInput, player1NameInput;


    private void Awake()
    {
        SetupInteractables();
    }

    private void SetupInteractables()
    {
        start.onClick.AddListener(() =>
        {
            int randomStartTurn = Random.Range(0, 2);
            gameObject.SetActive(false);
            TurnManager.instance.StartTurnGameplay(randomStartTurn);
            CardManager.instance.GenerateCards();
        });

        player0NameInput.onValueChanged.AddListener((name) =>
        {
            PlayerManager.instance.players[0].playerName = name;
            player0Name.text = name;
            player0InGameName.text = name;
        });
        player1NameInput.onValueChanged.AddListener((name) =>
        {
            PlayerManager.instance.players[1].playerName = name;
            player1Name.text = name;
            player1InGameName.text = name;
        });
    }

}
