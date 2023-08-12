using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Gamemodes GameMode;
    public Speeds Speed;
    public int State;


    void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            PlayerMovement movement = collision.gameObject.GetComponent<PlayerMovement>();
            movement.ChangeByPortal(GameMode,Speed,State);
        }
        catch{ }
    }
}
