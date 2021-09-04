using System.Collections;
using System.Collections.Generic;
using ToolBox.Injection;
using UnityEngine;

public class DependencyBehavior : MonoBehaviour
{
    protected virtual void Awake()
    {
        GlobalInjector.Injector.InjectDependencies(this);
    }
}
