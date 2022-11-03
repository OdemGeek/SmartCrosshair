using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SmartCrosshair
{
    [AddComponentMenu("")]
    public class Container : MonoBehaviour
    {
        public Image dot;
        public RectTransform dotRect;
        public Image right;
        public RectTransform rightRect;
        public Image left;
        public RectTransform leftRect;
        public Image up;
        public RectTransform upRect;
        public Image down;
        public RectTransform downRect;
    }
}
