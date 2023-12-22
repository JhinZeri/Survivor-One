using System;
using System.Collections.Generic;
using EnemyCodes;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        public Transform unlimitedPoint;

        public GameObject[] recyclablePrefabs;

        private List<GameObject>[] m_objectPoolLists;

        public int[] poolLength;

        private void Awake()
        {
            m_objectPoolLists = new List<GameObject>[recyclablePrefabs.Length];

            poolLength = new int[recyclablePrefabs.Length];

            for (int index = 0; index < m_objectPoolLists.Length; index++)
            {
                m_objectPoolLists[index] = new List<GameObject>();
            }

            // Debug.Log(m_objectPoolLists.Length);
        }


        public GameObject Spawn(int index)
        {
            GameObject select = null;

            foreach (var obj in m_objectPoolLists[index])
            {
                if (!obj.activeSelf)
                {
                    select = obj;

                    select.SetActive(true);

                    break;
                }
            }

            if (!select)
            {
                select = Instantiate(recyclablePrefabs[index]);

                m_objectPoolLists[index].Add(select);
            }

            UpdatePoolLength();

            return select;
        }

        public GameObject Spawn(int index, Transform parent)
        {
            GameObject select = null;

            foreach (var obj in m_objectPoolLists[index])
            {
                if (!obj.activeSelf)
                {
                    select = obj;

                    select.SetActive(true);

                    break;
                }
            }

            if (!select)
            {
                select = Instantiate(recyclablePrefabs[index], parent);

                m_objectPoolLists[index].Add(select);
            }

            UpdatePoolLength();

            return select;
        }

        public GameObject Spawn(int index, Vector3 position)
        {
            GameObject select = null;

            foreach (var obj in m_objectPoolLists[index])
            {
                if (!obj.activeSelf)
                {
                    select = obj;

                    select.transform.position = position;

                    select.SetActive(true);

                    break;
                }
            }

            if (!select)
            {
                select = Instantiate(recyclablePrefabs[index], position, Quaternion.identity);

                m_objectPoolLists[index].Add(select);
            }

            UpdatePoolLength();

            return select;
        }

        public GameObject Spawn(int index, Transform parent, Vector3 position)
        {
            GameObject select = null;

            foreach (var obj in m_objectPoolLists[index])
            {
                if (!obj.activeSelf)
                {
                    select = obj;

                    select.transform.position = position;

                    select.SetActive(true);

                    break;
                }
            }

            if (!select)
            {
                select = Instantiate(recyclablePrefabs[index], position, Quaternion.identity, parent);

                m_objectPoolLists[index].Add(select);
            }

            UpdatePoolLength();

            return select;
        }

        public void DeSpawn(GameObject obj)
        {
            if (obj.activeSelf)
            {
                if (obj.GetComponent<Rigidbody2D>())
                {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

                obj.SetActive(false);
                obj.transform.position = unlimitedPoint.position;
            }
        }

        private void UpdatePoolLength()
        {
            for (int index = 0; index < m_objectPoolLists.Length; index++)
            {
                poolLength[index] = m_objectPoolLists[index].Count;
            }
        }
    }
}