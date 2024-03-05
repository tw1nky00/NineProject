using Scripts.CounterScripts.CuttingCounterScripts;
using Scripts.CounterScripts.TrashCounterScripts;
using Scripts.CounterScripts;
using UnityEngine;

namespace Scripts.GeneralScripts
{
    public class ResetStaticDataManager : MonoBehaviour
    {
        private void Awake()
        {
            BaseCounter.ResetStaticData();
            CuttingCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
        }
    }
}