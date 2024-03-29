﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

public class testing : MonoBehaviour
{
    [SerializeField] private bool useJobs;
    [SerializeField] private Transform objects;
    private List<Objects> objectList;

    public class Objects
    {
        public Transform transform;
        public float moveY;
    }

    private void Start()
    {
        objectList = new List<Objects>();
        for (int i = 0; i < 1000; i++)
        {

        }
    }

    private void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        if (useJobs)
        {
            NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);
            for (int i = 0; i < 10; i++)
            {
                JobHandle jobHandle = ExampleThoughTaskJob();
                jobHandleList.Add(jobHandle);
                //jobHandle.Complete();
            }
            JobHandle.CompleteAll(jobHandleList);
            jobHandleList.Dispose();
        }
        else
        { 
            for (int i = 0; i < 10; i++)
            {
                ExampleThoughTask();
            }
        }
        Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
    }

    private void ExampleThoughTask()
    {
        //representes a though task like certain pathfinding or some complex calculation
        float value = 0f;
        for(int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }

    private JobHandle ExampleThoughTaskJob()
    {
        ExampleThoughJob job = new ExampleThoughJob();
        return job.Schedule();
    }
}

[BurstCompile]
public struct ExampleThoughJob : IJob
{
    //Extra fields added here

    public void Execute()
    {
        //representes a though task like certain pathfinding or some complex calculation
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}

public struct ExampleThoughList : IJobParallelFor
{
    void IJobParallelFor.Execute(int index)
    {
        
    }
}