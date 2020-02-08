﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadedJob : MonoBehaviour
{
    private bool m_IsDone = false;
    private object m_Handle = new object();
    private System.Threading.Thread m_Thread = null;
    public bool IsDone
    {
        get
        {
            bool tmp;
            lock (m_Handle)
            {
                tmp = m_IsDone;
            }
            return tmp;
        }
        set
        {
            lock (m_Handle)
            {
                m_IsDone = value;
            }
        }
    }

    // Creates the thread and calls the Run function within Start
    public virtual void Start()
    {
        m_Thread = new System.Threading.Thread(() => Run());
        m_Thread.Start();
    }

    // Aborts the thread is necessary
    public virtual void Abort()
    {
        m_Thread.Abort();
    }

    // Function to call when running
    protected virtual void ThreadFunction() { }

    // Function to call when finished doing the job
    protected virtual void OnFinished() { }

    public virtual bool Update()
    {
        if (IsDone)
        {
            OnFinished();
            return true;
        }
        return false;
    }

    public IEnumerator WaitFor()
    {
        while (!Update())
        {
            yield return null;
        }
    }

    // When a Job is running call the ThreadFunction that is overridden
    private void Run()
    {
        ThreadFunction();
        IsDone = true;
    }
}
