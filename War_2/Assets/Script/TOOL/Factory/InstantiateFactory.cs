using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Factory
{
    public class InstantiateFactory : MonoBehaviour
    {
        private List<Object> _resultList = new List<Object>();

        public List<Object> ObjectInstantiate(Object @object, int count)
        {
            _resultList.Clear();

            for (int i = 0; i < count; i++)
            {
                var obj = Instantiate(@object);
                _resultList.Add(obj);
            }

            return _resultList;
        }

        public List<Object> ObjectInstantiate(Object @object, int count, Transform parent)
        {
            _resultList.Clear();

            for (int i = 0; i < count; i++)
            {
                var obj = Instantiate(@object, parent);
                _resultList.Add(obj);
            }

            return _resultList;
        }

        public void ObjectInstantiate(Object @object, Transform parent)
        {
            Instantiate(@object, parent);
        }

        public void ObjectInstantiate<T>(GameObject @object, Transform parent)
        {
            Instantiate(@object, parent);
        }

        public T ObjectInstantiate<T>(GameObject @object, Transform parent, Vector3 spawnPoint)
        {
            GameObject obj = Instantiate(@object, spawnPoint, Quaternion.identity, parent);
            return obj.GetComponent<T>();
        }
    }
}
