using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ObjectPooler/PoolGroup", fileName = "NewPoolGroup")]
[System.Serializable]
public class PoolGroup : ScriptableObject
{
    public string groupTag;

    public List<Pool> poolsInGroup;
}
