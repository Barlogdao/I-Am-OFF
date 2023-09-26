using System;
using System.Linq;
using UnityEngine;

public class PlayerProvider : MonoBehaviour
{
    public static PlayerProvider Instance { get; private set; }

    [SerializeField] private PlayerSheet _playerSheet;


    public static Action PlayerChanged;

    private void Awake()
    {
        Instance = this;
    }

    public PlayerData[] GetAllPlayers()
    {
        return _playerSheet.Players
            .OrderBy(x => x.EarnType)
            .ThenBy(y => y.CoinCost)
            .ThenBy(z => z.ID)
            .ToArray();
    }

    public PlayerData GetPlayer(int id)
    {
        return _playerSheet.Players.First(x => x.ID == id);
    }
}
