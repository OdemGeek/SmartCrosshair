using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartCrosshair
{
    [CreateAssetMenu(fileName = "CrosshairConfiguration", menuName = "ScriptableObjects/CrosshairConfiguration", order = 1)]
    public class CrosshairConfiguration : ScriptableObject
    {
        [Tooltip("Don't work by now.")]
        public Mode mode = Mode.Static;
        public float distance = 3;
        public float width = 3;
        public float length = 15;
        public bool dot = true;
        public bool tshaped = false;
        public Color color = Color.white;
    }
    public enum Mode
    {
        StaticImage, Static, Dinamic
    }
}
