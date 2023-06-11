using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveProvider : MonoBehaviour
{
    public static SaveProvider Instace { get; private set; }

    private void Awake()
    {
        Instace = this;
    }


    public PlayerData CurrentPlayer;
}
