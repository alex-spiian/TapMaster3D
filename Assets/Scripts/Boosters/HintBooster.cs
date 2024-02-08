using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class HintBooster : MonoBehaviour
{
   [SerializeField] private LevelsSpawner _levelsSpawner;

   private List<CubeMover> _listAvailableCubes = new ();
   

   public void LookForAvailableCubes()
   {
      if (_listAvailableCubes.Count!=null)
      {
         _listAvailableCubes.Clear();
      }
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
   [UsedImplicitly]
   public void HighlightRandomCubes()
   {
      StartCoroutine(HighlightRandomCubesCoroutine());
   }

   public IEnumerator HighlightRandomCubesCoroutine()
   {
      for (var i = 0; i < _listAvailableCubes.Count; i++)
      {
         _listAvailableCubes[i].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
            yield return new WaitForSeconds(0.2f);
            if (_listAvailableCubes[i] != null)
            {
               _listAvailableCubes[i].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            }
            
      }
   }
}
