using System;
using System.Collections.Generic;
using EnemyCodes;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
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

        private void UpdatePoolLength()
        {
            for (int index = 0; index < m_objectPoolLists.Length; index++)
            {
                poolLength[index] = m_objectPoolLists[index].Count;
            }
        }
        
    }
}