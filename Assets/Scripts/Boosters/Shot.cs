using System.Collections;
using DefaultNamespace.SoundsManager;
using UnityEngine;
using UnityEngine.Serialization;

public class Shot : MonoBehaviour
{
    [SerializeField] private GameObject _objectToShoot; // Префаб объекта, который вы будете стрелять
    [SerializeField] private float _shootForce; // Сила выстрела 30
    [SerializeField] private int _maxShots; // Максимальное количество выстрелов 1
    [SerializeField] private int _timerToDestroy; // Таймер для отключения объекта лазер 7
    
    private int _shotsRemaining; // Количество оставшихся выстрелов
    private bool _canShoot=false; // Флаг, позволяющий выполнять выстрелы
    private SoundsManager _soundsManager;
    

    void Start()
    {
        _shotsRemaining = _maxShots; // Устанавливаем начальное количество выстрелов
        _soundsManager = Container.Instance.SoundsManager; // звук
    }

    void Update()
    {
        // Проверяем нажатие кнопки мыши и возможность стрелять
        if (Input.GetMouseButtonDown(0) && _canShoot && _shotsRemaining > 0)
        {
            // Создаем луч из позиции мыши в мировом пространстве
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем, попал ли луч в какой-либо объект
            if (Physics.Raycast(ray, out hit))
            {
                // Если луч попал в объект, создаем выстрел
                GameObject shotObject = Instantiate(_objectToShoot, transform.position, Quaternion.identity);

                // Запускаем корутину с задержкой в 3 секунды
                StartCoroutine(DestroyObjectAfterDelay(shotObject, _timerToDestroy));

                // Определяем направление выстрела в сторону попадания луча
                Vector3 shootDirection = (hit.point - transform.position).normalized;

                // Получаем компонент Rigidbody объекта, чтобы применить к нему силу
                Rigidbody rb = shotObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Применяем силу в заданном направлении
                    rb.AddForce(shootDirection * _shootForce, ForceMode.Impulse);

                    // Уменьшаем количество оставшихся выстрелов
                    _shotsRemaining--;
                    
                    _soundsManager.PlayShotLaser(); // звук выстрела
                }
            }
        }

        // Проверяем, достигнуто ли максимальное количество выстрелов
        if (_shotsRemaining <= 0)
        {
            _canShoot = false; // Отключаем возможность выстрелов
        }
    }

    IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // Ждем указанное количество секунд

        // Уничтожаем объект
        Destroy(obj);
    }

    // Активируем кнопкой бустер и восстанавливаем заряды и флаги
    public void ActiveShot()
    {
        _canShoot = true;
        _shotsRemaining = _maxShots;
    }
}