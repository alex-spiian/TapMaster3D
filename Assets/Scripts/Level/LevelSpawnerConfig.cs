using Level;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelSpawnerConfig", fileName = "LevelSpawnerConfig", order = 0)]
public class LevelSpawnerConfig : ScriptableObject
{
    [field:SerializeField] public float CubeFallingDuration { get; private set; }
    [field:SerializeField] public float MinDelayBetweenDrops { get; private set; }
    [field:SerializeField] public float MaxDelayBetweenDrops { get; private set; }
    [field:SerializeField] public float YValueToBeOutOfCamera { get; private set; }
    [field:SerializeField] public Vector3 SpawnPoint { get; private set; }
}
