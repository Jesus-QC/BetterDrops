using System.Collections.Generic;
using Exiled.API.Features.Items;
using UnityEngine;
using MEC;

namespace BetterDrops.Features.Components
{
    public class DropController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private bool _collided;
        private bool _crateOpened;
        
        public GameObject balloon;
        public List<GameObject> faces;

        private void Start()
        {
            ChangeLayers(transform, BetterDrops.Cfg.DropLayer);
            
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.mass = 20;
            _rigidbody.drag = 3;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }

        private void Update()
        {
            if (!_collided)
                transform.localEulerAngles += Vector3.up * Time.deltaTime * 30;
        }

        private void OnCollisionEnter(Collision _)
        {
            if(_collided) // The box has 4 simultaneous collisions
                return;
            
            _collided = true;
            Destroy(gameObject.GetComponent<Rigidbody>());
            
            balloon.AddComponent<BalloonController>();

            AddTrigger();
        }

        private void AddTrigger()
        {
            var collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = Vector3.one * 7.5f;
            collider.center = Vector3.up;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!_collided || _crateOpened || other.gameObject.name != "Player")
                return;

            _crateOpened = true;

            var possibleItems = BetterDrops.Cfg.PossibleItems;
            Item.Create(possibleItems[Random.Range(0, possibleItems.Count)]).Spawn(transform.position);
            
            foreach (var face in faces)
            {
                var r = face.AddComponent<Rigidbody>();
                r.AddExplosionForce(20, transform.position, 1);
            }

            BetterDrops.EventManager.Coroutines.Add(Timing.CallDelayed(5, () => Destroy(gameObject)));
        }
        
        private static void ChangeLayers(Transform t, int layer)
        {
            t.gameObject.layer = layer;
            foreach (Transform child in t)
            {
                ChangeLayers(child, layer);
            }
        }
    }
}