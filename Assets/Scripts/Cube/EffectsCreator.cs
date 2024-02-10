using UnityEngine;
using UnityEngine.Serialization;

namespace Cube
{
    public class EffectsCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;

        public void AddTrailOnCube(CubeMover[] cubes)
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                var effect = Instantiate(_effectPrefab);
                effect.transform.SetParent(cubes[i].transform, false);
            }
        }
    }
}