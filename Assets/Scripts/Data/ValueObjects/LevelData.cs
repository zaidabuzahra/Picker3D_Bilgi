using System.Collections.Generic;
using System;
using UnityEngine;

namespace Data.ValueObjects
{

    [Serializable]
    public class LevelData
    {
        public List<PoolData> PoolList = new List<PoolData>();
    }

    [Serializable]
    public struct PoolData
    {
        public sbyte RequiredObjectCount;
    }

}