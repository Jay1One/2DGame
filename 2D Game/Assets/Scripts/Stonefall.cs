using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Random = UnityEngine.Random;

public class Stonefall : MonoBehaviour
{
    [SerializeField] private GameObject m_StonePrefab;
    [SerializeField] private float m_MinStonefallInterval = 0.5f;
    [SerializeField] private float m_MaxStonefallInterval = 2f;
    private float nextStoneTimer = 0f;
    private bool isSeenByCamera;

    private void Start()
    {
        nextStoneTimer = Random.Range(m_MinStonefallInterval, m_MaxStonefallInterval) + Time.time;
    }


    private void Update()
    {
        if (Time.time >= nextStoneTimer)
        {
            Instantiate(m_StonePrefab,transform.position,quaternion.identity);
            nextStoneTimer = Random.Range(m_MinStonefallInterval, m_MaxStonefallInterval) + Time.time; 
        }
    }
}
