﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject goPrefab;
    public int count;
    public Transform trPoolParent;
}
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private ObjectInfo[] objectInfo = null;
    public static ObjectPool instance;
    public Queue<GameObject> noteQueue=new Queue<GameObject>();
    void Start()
    {
        instance = this;
        noteQueue = InsertQueue(objectInfo[0]);
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo)
    {
        Queue<GameObject> t_queue=new Queue<GameObject>();
        for (int i = 0; i < p_objectInfo.count; i++)
        {
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);
            if(p_objectInfo.trPoolParent!=null)
                t_clone.transform.SetParent(p_objectInfo.trPoolParent);
            else
                t_clone.transform.SetParent(this.transform);
            
            t_queue.Enqueue(t_clone);
        }
        return t_queue;
    }
}
