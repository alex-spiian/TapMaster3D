using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, срабатывает ли триггер на объекте, который движется
        if (other.gameObject.CompareTag("Cube"))
        {
            DestroyObject(other.gameObject);
            Debug.Log("Попал в триггер");
        }
    }

    // Метод для разрушения объекта
    private void DestroyObject(GameObject obj)
    {
        Destroy(obj);
        Debug.Log("Разрушен");
    }
}