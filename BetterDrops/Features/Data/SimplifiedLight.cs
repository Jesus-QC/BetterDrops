using AdminToys;
using Mirror;
using UnityEngine;

namespace BetterDrops.Features.Data
{
    public class SimplifiedLight
    {
        private static LightSourceToy _lightPrefab;
        private static LightSourceToy LightPrefab
        {
            get
            {
                if (_lightPrefab == null)
                {
                    foreach (var gameObject in NetworkClient.prefabs.Values)
                        if (gameObject.TryGetComponent<LightSourceToy>(out var component))
                            _lightPrefab = component;
                }

                return _lightPrefab;
            }
        }

        private Vector3 _position;
        private Color _color;

        public SimplifiedLight(Vector3 position, Color color)
        {
            _position = position;
            _color = color;
        }

        public GameObject Spawn(Transform parent)
        {
            var light = Object.Instantiate(LightPrefab, parent);

            light.transform.localPosition = _position;
            light.NetworkMovementSmoothing = 60;
            light.NetworkLightColor = _color;
            
            NetworkServer.Spawn(light.gameObject);

            return light.gameObject;
        }
    }
}