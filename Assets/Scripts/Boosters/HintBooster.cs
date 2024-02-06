using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HintBooster : MonoBehaviour
{
   [SerializeField] private LevelsSpawner _levelsSpawner;
   [SerializeField] private ParticleSystem _particle;

   private List<CubeMover> _listAvailableCubes = new ();
   

   private void LookForAvailableCubes()
   {
      var allCubes = _levelsSpawner._allCubesOfCurrentLevel;
      for (var i = 0; i < allCubes.Length; i++)
      {
         if (allCubes[i]!=null&& !allCubes[i].IsMoving)
         {
            if (allCubes[i].IsWayFree())
            {
               _listAvailableCubes.Add(allCubes[i]);
            }
            
         }
      }
   }

   private void HighlightRandomCubes()
   {
      var randomIndex = Random.Range(0, _listAvailableCubes.Count);
      var cube = _listAvailableCubes[randomIndex];
      var particleAura = Instantiate(_particle);
      particleAura.transform.SetParent(cube.transform);
      cube.GetComponent<Outline>();

   }

   public void ActivateHintBooster()
   {
      LookForAvailableCubes();
      HighlightRandomCubes();
   }
   
}
