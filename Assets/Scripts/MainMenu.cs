using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;

    public void StartGame()
    {
        inputHandler.StartGame();
        gameObject.SetActive(false);
    }

}
