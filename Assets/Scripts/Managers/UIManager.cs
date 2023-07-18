using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIManager instance;
    public GameEndUIController gameEndUI;

    private void Awake()
    {
        gameEndUI.gameObject.SetActive(false);
        instance = this;
    }

    public void GameFinished(Player winner)
    {
        gameEndUI.gameObject.SetActive(true);
        gameEndUI.Initialize(winner);
    }

}
