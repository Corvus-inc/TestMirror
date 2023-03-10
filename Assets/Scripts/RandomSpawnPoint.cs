using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class RandomSpawnPoint : MonoBehaviour
{
    private readonly List<int> _numList = new List<int>();
    private readonly List<Transform> _points = new List<Transform>();

    private void Awake()
    {
        FindAllPoints();
        ShuffleNumOrder();
        ReRegisterIndexOrder();
    }

    private void FindAllPoints()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var tr = transform.GetChild(i);
            _numList.Add(tr.GetSiblingIndex());
            _points.Add(tr);
        }
    }

    private void ShuffleNumOrder()
    {
        for (int i = 0; i < _numList.Count; i++)
        {
            var rnd = Random.Range(0, _numList.Count);
            (_numList[rnd], _numList[i]) = (_numList[i], _numList[rnd]);
        }
    }

    private void ReRegisterIndexOrder()
    {
        var count = 0;

        foreach (var num in _numList)
        {
            if (_points[num].TryGetComponent(out NetworkStartPosition startPos))
                Destroy(startPos);

            _points[num].AddComponent<NetworkStartPosition>();
            _points[num].SetSiblingIndex(count++);
        }
    }
}