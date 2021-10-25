using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private GameObject WinScreen;

    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    private static GameController _instance;
    // Start is called before the first frame update
    public void ShowLoseScreen()
    {
        LoseScreen.SetActive(true);
    }
    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
    }
    private void Start()
    {
        _instance = this;
    }
}
