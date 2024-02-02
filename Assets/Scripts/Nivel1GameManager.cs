using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;

    public void GameOver()
    {
        if (!isGameOver)
            isGameOver = true;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
