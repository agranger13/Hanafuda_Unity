using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<Player> players = new();

    private void Awake()
    {
        instance = this;
    }

    internal void AssignTurn(int currentPlayerTurn)
    {
        FindPlayerByID(currentPlayerTurn).myTurn = true;
    }

    public Player FindPlayerByID(int ID)
    {
        return players.Find(x => x.ID == ID);
    }
}
