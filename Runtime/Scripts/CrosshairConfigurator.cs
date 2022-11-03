using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartCrosshair
{
    public class CrosshairConfigurator : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool _useGlobalSettings = true;
        [SerializeField] private CrosshairConfiguration config;

        private static CrosshairConfiguration s_globalConfig;
        private static List<CrosshairConfigurator> instances;

        private Container _container;
        #endregion

        #region Config set
        /// <summary>
        /// Set config to object
        /// </summary>
        /// <param name="_config">Config to set</param>
        public void SetConfig(CrosshairConfiguration _config)
        {
            config = _config;
            s_globalConfig = config;
            ApplyCrosshair();
        }

        /// <summary>
        /// Create, set and get config
        /// </summary>
        /// <param name="_mode"></param>
        /// <param name="_distance"></param>
        /// <param name="_width"></param>
        /// <param name="_length"></param>
        /// <param name="_dot"></param>
        /// <param name="_tshaped"></param>
        /// <param name="_color"></param>
        public CrosshairConfiguration SetConfig(Mode _mode, float _distance, float _width, float _length, bool _dot, bool _tshaped, Color _color)
        {
            config = new CrosshairConfiguration();
            config.mode = _mode;
            config.distance = _distance;
            config.width = _width;
            config.length = _length;
            config.dot = _dot;
            config.tshaped = _tshaped;
            config.color = _color;
            s_globalConfig = config;
            ApplyCrosshair();
            return config;
        }
        #endregion

        #region Global config set
        /// <summary>
        /// Set global config
        /// </summary>
        /// <param name="_config">Config to set</param>
        public static void SetGlobalConfig(CrosshairConfiguration _config)
        {
            s_globalConfig = _config;
            ApplyGlobalConfig();
        }

        /// <summary>
        /// Create, set and get global config
        /// </summary>
        /// <param name="_mode"></param>
        /// <param name="_distance"></param>
        /// <param name="_width"></param>
        /// <param name="_length"></param>
        /// <param name="_dot"></param>
        /// <param name="_tshaped"></param>
        /// <param name="_color"></param>
        public static void SetGlobalConfig(Mode _mode, float _distance, float _width, float _length, bool _dot, bool _tshaped, Color _color)
        {
            s_globalConfig = new CrosshairConfiguration();
            s_globalConfig.mode = _mode;
            s_globalConfig.distance = _distance;
            s_globalConfig.width = _width;
            s_globalConfig.length = _length;
            s_globalConfig.dot = _dot;
            s_globalConfig.tshaped = _tshaped;
            s_globalConfig.color = _color;
            ApplyGlobalConfig();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Update values for UI
        /// </summary>
        public void ApplyCrosshair()
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
        #endregion

        #region Private methods
        /// <summary>
        /// Apply global config to all instances
        /// </summary>
        private static void ApplyGlobalConfig()
        {
            CleanupInstances();
            foreach (var item in instances)
                if (item._useGlobalSettings) item.config = s_globalConfig;
        }

        private void Start()
        {
            // If we should use global config and it's not null set local config to it
            if (s_globalConfig != null && _useGlobalSettings)
            {
                config = s_globalConfig;
            }
            Initialize();
        }

        private void Initialize()
        {
            _container = GetComponentInChildren<Container>();
            CleanupInstances();
            instances.Add(this);
        }

        /// <summary>
        /// Clean instances list from null objects
        /// </summary>
        private static void CleanupInstances()
        {
            List<CrosshairConfigurator> temp = instances;
            instances.Clear();
            foreach (CrosshairConfigurator item in temp)
                if (item != null) instances.Add(item);
        }
        #endregion
    }
}
