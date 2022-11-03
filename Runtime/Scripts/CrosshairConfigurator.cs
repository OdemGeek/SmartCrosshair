using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartCrosshair
{
    public class CrosshairConfigurator : MonoBehaviour
    {
        [SerializeField] private CrosshairConfiguration config;

        [SerializeField] private bool _updateCrosshair = false;

        private Container _container;
        private bool _initialized = false;

        public void ApplySettings(CrosshairConfiguration _config)
        {
            config = _config;
            ApplyCrosshair();
        }

        public void ApplySettings(Mode _mode, float _distance, float _width, float _length, bool _dot, bool _tshaped, Color _color)
        {
            config = new CrosshairConfiguration();
            config.mode = _mode;
            config.distance = _distance;
            config.width = _width;
            config.length = _length;
            config.dot = _dot;
            config.tshaped = _tshaped;
            config.color = _color;
            ApplyCrosshair();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _container = GetComponentInChildren<Container>();
            _initialized = true;
        }

        private void ApplyCrosshair()
        {
#if UNITY_EDITOR
            if (_container == null) _container = GetComponentInChildren<Container>();
#endif
            // Apply distance from center
            _container.rightRect.anchoredPosition = new Vector2(config.distance, 0);
            _container.leftRect.anchoredPosition = new Vector2(-config.distance, 0);
            _container.upRect.anchoredPosition = new Vector2(0, config.distance);
            _container.downRect.anchoredPosition = new Vector2(0, -config.distance);

            // Apply width
            _container.dotRect.sizeDelta = new Vector2(config.width, config.width);

            // Apply length
            _container.rightRect.sizeDelta = new Vector2(config.length, 0);
            _container.leftRect.sizeDelta = new Vector2(config.length, 0);
            _container.upRect.sizeDelta = new Vector2(0, config.length);
            _container.downRect.sizeDelta = new Vector2(0, config.length);

            // Apply color
            _container.dot.color = config.color;
            _container.right.color = config.color;
            _container.left.color = config.color;
            _container.up.color = config.color;
            _container.down.color = config.color;

            // Apply special params
            // If current state is different from congig change it
            if (config.dot != _container.dot.enabled) _container.dot.enabled = config.dot;
            if (config.tshaped != !_container.up.enabled) _container.up.enabled = !config.tshaped;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            ApplyCrosshair();
            _updateCrosshair = false;
        }
#endif
    }
}
