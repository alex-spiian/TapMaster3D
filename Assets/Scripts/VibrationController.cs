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
                if (GUI.Button(new Rect(0, 10, 100, 32), "Vibrate!"))
                {
                    Handheld.Vibrate();
                }
            }
        }
    }
}