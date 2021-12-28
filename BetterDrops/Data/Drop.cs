using System.Collections.Generic;
using BetterDrops.Features.Components;
using UnityEngine;

namespace BetterDrops.Data
{
    public class Drop
    {
        private readonly GameObject _gameObject;
        private readonly List<GameObject> _faces = new List<GameObject>();
        private GameObject _balloon;

        public Drop(Vector3 position)
        {
            _gameObject = new GameObject("Drop");
            _gameObject.transform.position = position;
        }

        public void Spawn()
        {
            var transform = _gameObject.transform;

            for (float i = -0.5f; i < 1; i++)
            {
                _faces.Add(new Face(new Vector3(i,0,0), Vector3.up * 90, transform).GameObject);
                _faces.Add(new Face(new Vector3(0,i,0), Vector3.right * 90, transform).GameObject);
                _faces.Add(new Face(new Vector3(0,0,i), Vector3.zero, transform).GameObject);
            }
            
            var rndColor = Random.ColorHSV();
            _balloon = new SimplifiedToy(PrimitiveType.Sphere, Vector3.up * 2.125f, Vector3.zero, Vector3.one * -2, rndColor).Spawn(transform);
            new SimplifiedLight(Vector3.zero, rndColor).Spawn(_balloon.transform).transform.SetParent(_balloon.transform);
            
            var scale = new Vector3(-0.01f, -1, -0.01f);
            
            for (float i = -0.5f; i < 1; i++)
            for (float j = -0.5f; j < 1; j++)
                new SimplifiedToy(PrimitiveType.Cylinder, new Vector3(i, 1, j), new Vector3(0, 100 * j * (i * -2), -20 * i), scale, Color.white).Spawn(transform).transform.SetParent(_balloon.transform);

            var controller = _gameObject.AddComponent<DropController>();
            controller.balloon = _balloon;
            controller.faces = _faces;
        }

        private class Face
        {
            public GameObject GameObject;
            private List<GameObject> _childs = new List<GameObject>();

            public Face(Vector3 pos, Vector3 rot, Transform drop)
            {
                GameObject = new GameObject("Face");
                GameObject.transform.SetParent(drop, false);
                
                GameObject.transform.localPosition = pos;
                GameObject.transform.localEulerAngles = rot;
                
                var parent = GameObject.transform;

                var scale = new Vector3(1, 0.2f, 0.125f);

                for (float i = -0.5f; i < 1; i++)
                {
                    _childs.Add(new SimplifiedToy(PrimitiveType.Cube, new Vector3(i, 0, 0), Vector3.forward * 90, scale, Color.black).Spawn(parent));
                    _childs.Add(new SimplifiedToy(PrimitiveType.Cube, new Vector3(0, i, 0), Vector3.zero, scale, Color.black).Spawn(parent));
                }
                
                _childs.Add(new SimplifiedToy(PrimitiveType.Cube, Vector3.zero, Vector3.zero, new Vector3(1,1,0.1f), Color.gray).Spawn(parent));
                _childs.Add(new SimplifiedToy(PrimitiveType.Cube, Vector3.zero, Vector3.forward * 45, new Vector3(1.2f, 0.2f, 0.125f), Color.black).Spawn(parent));
            }
        }
    }
}