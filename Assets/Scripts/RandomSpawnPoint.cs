using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class RandomSpawnPoint : NetworkBehaviour
{
    private readonly List<int> _numList = new List<int>();
    private readonly List<Transform> _points = new List<Transform>();

    private int _currentIndex;
    private void Awake()
    {
        FindAllPoints();
    }

    private void FindAllPoints()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var tr = transform.GetChild(i);
            _numList.Add(tr.GetSiblingIndex());
            _points.Add(tr);
            
            tr.AddComponent<NetworkStartPosition>();
        }
    }
    public override void OnStartClient()
    {
        Debug.Log($"START Client {NetworkManager.startPositionIndex}");
        NetworkManager.UnRegisterStartPosition(NetworkManager.startPositions[NetworkManager.startPositionIndex]);
    }

    public override void OnStopClient()
    {
        Debug.Log($"STOP Client {NetworkManager.startPositionIndex}");
        NetworkManager.RegisterStartPosition(NetworkManager.startPositions[NetworkManager.startPositionIndex]);
    }

    #region Shuffle

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

    #endregion
    
}