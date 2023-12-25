using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{

    private List<T> Pool = new List<T>();
    private Stack<T> _deactivePool = new Stack<T>();

    private GameObject _prefab;

    public ObjectPool(GameObject pooledObject, int amount)
    {
        this._prefab = pooledObject;
        SpawnObject(amount);
    }
    public ObjectPool(GameObject pooledObject, Transform parent, float spawnPoint, int amount)
    {
        this._prefab = pooledObject;
        SpawnRandom(amount, parent, spawnPoint);
    }

    public T GetPoolObject()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            T temp = Pool[i];
            if (!temp.gameObject.activeInHierarchy)
            {
                return temp;
            }
        }
        return null;
    }
    public void SpawnObject(int amount)
    {
        T t;
        for (int i = 0; i < amount; i++)
        {
            t = GameObject.Instantiate(_prefab).GetComponent<T>();
            t.gameObject.SetActive(false);
            Pool.Add(t);
        }
    }
    public void Respawn(float spawnPoint)
    {
        T temp = _deactivePool.Pop();
        Vector3 randomPos = new Vector3(Random.Range(-spawnPoint, spawnPoint), 0f, Random.Range(-spawnPoint, spawnPoint));
        temp.transform.position = randomPos;
        temp.gameObject.SetActive(true);
    }
    public void PushObjectBack(T t)
    {
        t.gameObject.SetActive(false);
        _deactivePool.Push(t);
    }
    public void SpawnRandom(int amount, Transform parent, float spawnPoint)
    {
        T t;
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-spawnPoint, spawnPoint), 0f, Random.Range(-spawnPoint, spawnPoint));
            t = GameObject.Instantiate(_prefab, parent).GetComponent<T>();
            t.transform.position = randomPos;
            t.gameObject.SetActive(true);
            Pool.Add(t);
        }
    }
    public int DeactiveObjectCount()
    {
        return _deactivePool.Count;
    }
}
