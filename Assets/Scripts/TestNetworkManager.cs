using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TestNetworkManager : NetworkManager
{
    public override Transform GetStartPosition()
    {
        startPositions.RemoveAll(t => t == null);

        if (startPositions.Count == 0)
            return null;

        if (playerSpawnMethod == PlayerSpawnMethod.Random)
        {
            var index = Random.Range(0, startPositions.Count);
            startPositionIndex = index;
            return startPositions[index];
        }
        else
        {
            Transform startPosition = startPositions[startPositionIndex];
            startPositionIndex = (startPositionIndex + 1) % startPositions.Count;
            return startPosition;
        }
    }
}
