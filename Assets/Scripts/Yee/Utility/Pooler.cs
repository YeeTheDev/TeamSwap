using System.Collections.Generic;
using UnityEngine;

namespace Yee.Utility
{
    public class Pooler : MonoBehaviour
    {
        [SerializeField] int initialAmount;
        [SerializeField] bool createsMore = true;
        [SerializeField] GameObject objectToPool;

        Queue<GameObject> pooledObjects = new Queue<GameObject>();

        private void Awake()
        {
            for (int i = 0; i < initialAmount; i++)
            {
                Enqueue(CreateObject());
            }
        }

        private GameObject CreateObject()
        {
            GameObject pooledObject = Instantiate(objectToPool, transform.position, Quaternion.identity, transform);

            return pooledObject;
        }

        public bool TryGetObject(out GameObject pooledObject)
        {
            pooledObject = pooledObjects.Count > 0 ? pooledObjects.Dequeue() : createsMore ? CreateObject() : null;

            return pooledObject != null;
        }

        public void Enqueue(GameObject toEnqueue)
        {
            if (pooledObjects.Contains(toEnqueue)) { return; }

            pooledObjects.Enqueue(toEnqueue);
        }
    }
}