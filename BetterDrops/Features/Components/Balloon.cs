using UnityEngine;

namespace BetterDrops.Features.Components
{
    public class Balloon : MonoBehaviour
    {
        private float _startPos;

        private void Start() => _startPos = transform.position.y;

        private void Update()
        {
            if (transform.position.y - _startPos < 15)
            {
                transform.position += Vector3.up * Time.deltaTime * 10;
                transform.localScale -= Vector3.one * Time.deltaTime * 1.25f;
            }
            else
                Destroy(gameObject);
        }
    }
}