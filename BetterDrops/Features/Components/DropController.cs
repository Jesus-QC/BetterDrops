using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Toys;
using MEC;
using UnityEngine;
using Light = Exiled.API.Features.Toys.Light;

namespace BetterDrops.Features.Components
{
    public class DropController: MonoBehaviour
    {
        private Color _color;
        private IEnumerable<ItemType> _items;

        public void Init(Color mainColor, IEnumerable<ItemType> items)
        {
            _color = mainColor;
            _items = items;
            
            Transform t = transform;
            
            SpawnString(t, new Vector3(.5f,1,.5f), new Vector3(7,0,-7));
            SpawnString(t, new Vector3(-.5f,1,.5f), new Vector3(7,0,7));
            SpawnString(t, new Vector3(.5f,1,-.5f), new Vector3(-7,0,-7));
            SpawnString(t, new Vector3(-.5f,1,-.5f), new Vector3(-7,0,7));

            SpawnFace(t, new Vector3(0.5f, 0, 0), new Vector3(0,0,0));
            SpawnFace(t, new Vector3(0, 0, 0.5f), new Vector3(0,90,0));
            SpawnFace(t, new Vector3(-0.5f, 0, 0), new Vector3(0,0,0));
            SpawnFace(t, new Vector3(0, 0, -0.5f), new Vector3(0,90,0));
            SpawnFace(t, new Vector3(0, 0.5f, 0), new Vector3(0,0,90));
            SpawnFace(t, new Vector3(0, -0.5f, 0), new Vector3(0,0,90));
            
            SpawnBalloon(t);
            SpawnLight(t);
            
            Rigidbody r = t.gameObject.AddComponent<Rigidbody>();
            r.drag = 10;
            r.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

            Timing.RunCoroutine(ChangeLayers());
        }

        private IEnumerator<float> ChangeLayers()
        {
            yield return Timing.WaitForOneFrame;
            ChangeLayers(transform, 6);
        }

        private void SpawnBalloon(Transform parent)
        {
            CreateToy(PrimitiveType.Sphere, parent, new Vector3(0,2,0), Vector3.zero, new Vector3(-2,-2,-2), _color);
        }

        private void SpawnString(Transform parent, Vector3 position, Vector3 rotation)
        {
            CreateToy(PrimitiveType.Cylinder, parent, position, rotation, new Vector3(-.05f,-1,-.05f), Color.white);
        }
        
        private void SpawnFace(Transform parent, Vector3 position, Vector3 rotation)
        {
            Transform f = new GameObject("face").transform;
            f.SetParent(parent);
            f.localPosition = position;
            f.localRotation = Quaternion.Euler(rotation);
            
            SpawnBigFace(f);
            SpawnFacePart(f, Vector3.zero);
            SpawnFacePart(f, new Vector3(0,0,-0.3f));
            SpawnFacePart(f, new Vector3(0,0,0.3f));
        }

        private void SpawnBigFace(Transform parent)
        {
            CreateToy(PrimitiveType.Cube, parent, Vector3.zero, Vector3.zero, new Vector3(-.1f, -1, -1), _color);
        }
        
        private void SpawnFacePart(Transform parent, Vector3 localPosition)
        {
            CreateToy(PrimitiveType.Cube, parent, localPosition, Vector3.zero, new Vector3(-.2f, 1.2f, -.2f), Color.black);
        }

        private void SpawnLight(Transform parent)
        {
            Light l = Light.Create(null, null, null, false);
            l.Color = _color;
            l.MovementSmoothing = 60;
            l.Intensity = 2;
            Transform t = l.Base.transform;
            t.SetParent(parent);
            t.localPosition = Vector3.up;
            l.Spawn();
        }
        
        private static void CreateToy(PrimitiveType type, Transform parent, Vector3 pos, Vector3 rot, Vector3 scale, Color color)
        {
            Primitive p = Primitive.Create(type, null, null, scale, false);
            Transform t = p.Base.transform;
            t.SetParent(parent);
            t.localPosition = pos;
            t.localRotation = Quaternion.Euler(rot);
            p.Color = color;
            p.MovementSmoothing = 60;
            p.Spawn();
        }

        private static void ChangeLayers(Transform t, int layer)
        {
            Log.Info(t.name + ' ' + layer);
            t.gameObject.layer = layer;
            foreach (Transform child in t)
            {
                ChangeLayers(child, layer);
            }
        }

        private void Update()
        {
            if (!_collided)
                transform.localEulerAngles += Vector3.up * Time.deltaTime * 30;
        }

        private bool _collided;
        
        private void OnCollisionEnter()
        {
            if (_collided)
                return;

            _collided = true;
            
            DeployBalloon();
            AddTrigger();
        }

        private void DeployBalloon()
        {
            Destroy(transform.GetChild(0).gameObject);
            Destroy(transform.GetChild(1).gameObject);
            Destroy(transform.GetChild(2).gameObject);
            Destroy(transform.GetChild(3).gameObject);
            transform.GetChild(10).gameObject.AddComponent<BalloonController>();
        }

        private void AddTrigger()
        {
            BoxCollider c = gameObject.AddComponent<BoxCollider>();
            c.isTrigger = true;
            c.size = Vector3.one * 7.5f;
            c.center = Vector3.up;
        }

        private bool _triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered || !_collided || other.gameObject.name != "Player")
                return;

            _triggered = true;

            Transform t = transform;
            
            for (int i = 0; i < t.childCount - 2; i++)
            {
                t.GetChild(i).gameObject.AddComponent<DisappearController>().startPos = t.position;
            }

            foreach (ItemType item in _items)
                Item.Create(item).Spawn(t.position);

            Destroy(GetComponent<Rigidbody>());
            Destroy(gameObject, 5);
        }
    }
}