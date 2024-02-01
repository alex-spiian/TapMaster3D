using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
