using Mirror;
using AdminToys;
using UnityEngine;

namespace BetterDrops.Data
{
    public class SimplifiedToy
    {
        private static PrimitiveObjectToy _toyPrefab;
        private static PrimitiveObjectToy ToyPrefab
        {
            get
            {
                if (_toyPrefab == null)
                {
                    foreach (var gameObject in NetworkClient.prefabs.Values)
                        if (gameObject.TryGetComponent<PrimitiveObjectToy>(out var component))
                            _toyPrefab = component;
                }

                return _toyPrefab;
            }
        }
        
        private PrimitiveType _type;
        private Vector3 _position;
        private Vector3 _rotation;
        private Vector3 _scale;
        private Color _color;

        public SimplifiedToy(PrimitiveType type, Vector3 position, Vector3 rotation, Vector3 scale, Color color)
        {
            _type = type;
            _position = position;
            _rotation = rotation;
            _scale = scale;
            _color = color;
        }
        
        public GameObject Spawn(Transform parent)
        {
            var toy = Object.Instantiate(ToyPrefab, parent);
            
            toy.NetworkPrimitiveType = _type;
            
            toy.transform.localPosition = _position;
            toy.transform.localEulerAngles = _rotation;
            toy.transform.localScale = _scale;
            
            toy.NetworkScale = _scale; // Fix collision smh (it needs both localScale and networkScale for some reason)
            toy.NetworkMaterialColor = _color;
            toy.NetworkMovementSmoothing = 60;

            NetworkServer.Spawn(toy.gameObject);

            return toy.gameObject;
        }
    }
}