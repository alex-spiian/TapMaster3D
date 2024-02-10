using UnityEngine;
using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{
    public class VibrationController : MonoBehaviour
    {
        public void OnGUI()
        {
            if (SystemInfo.supportsVibration)
            {
                Handheld.Vibrate();
            }
        }
    }
}