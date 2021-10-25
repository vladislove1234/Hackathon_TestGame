using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerFacade>() != null)
        {
            GameController.Instance.ShowWinScreen();
            collision.gameObject.GetComponent<IMove>().StopMove();
        }

    }
}
