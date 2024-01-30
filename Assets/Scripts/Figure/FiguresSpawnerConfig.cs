using Level;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FiguresSpawnerConfig", fileName = "FiguresSpawnerConfig", order = 0)]
public class FiguresSpawnerConfig : ScriptableObject
{
    [field:SerializeField] public float CubeFallingDuration { get; private set; }
    [field:SerializeField] public float MinDelayBetweenDrops { get; private set; }
    [field:SerializeField] public float MaxDelayBetweenDrops { get; private set; }
    [field:SerializeField] public float YValueToBeOutOfCamera { get; private set; }
}
