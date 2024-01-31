using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    public void SetObjects()
    {
        foreach (GameObject gameObject in _objects)
        {
            // Если игровой объект активен, то выключаем его, и наоборот
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}