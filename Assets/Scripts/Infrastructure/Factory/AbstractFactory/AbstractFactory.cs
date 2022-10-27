using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;
using KasherOriginal.Factories.AbstractFactory;
using Object = UnityEngine.Object;

public class AbstractFactory : IAbstractFactory
{
    public AbstractFactory(DiContainer container)
    {
        _container = container;
    }

    private DiContainer _container;
    
    private List<GameObject> _instances = new List<GameObject>();

    public List<GameObject> Instances
    {
        get => _instances;
    }

    public GameObject CreateInstance(GameObject prefab, Vector3 spawnPoint)
    {
        var instance = _container.InstantiatePrefab(prefab, spawnPoint, Quaternion.identity, null);
        
        _instances.Add(instance);

        return instance;
    }

    public void DestroyInstance(GameObject instance)
    {
        if (instance == null)
        {
            throw new NullReferenceException("There is no instance to destroy");
        }
        
        if (!_instances.Contains(instance))
        {
            throw new NullReferenceException($"Instance {instance} can't be destroyed, cause there is no {instance} on Abstract Factory Instances");
        }
        
        Object.Destroy(instance);
        _instances.Remove(instance);
    }

    public void DestroyAllInstances()
    {
        for (int i = 0; i < _instances.Count; i++)
        {
            Object.Destroy(_instances[i]);
        }
        
        _instances.Clear();
    }
}
