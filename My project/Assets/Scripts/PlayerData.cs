using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public float[] playerStats;
    public float[] playerPositionAndRotation;
    public string[] inventoryContent;
    public string[] quickSlotContent;
    //public float[] inventoryContent;

    public PlayerData(float[] _playerStats, float[] _playerPosAndRot, string[] _inventoryContent, string[] _quickSlotContent)
    {
        playerStats = _playerStats;
        playerPositionAndRotation = _playerPosAndRot;
        inventoryContent = _inventoryContent;
        quickSlotContent = _quickSlotContent;
    }
}
