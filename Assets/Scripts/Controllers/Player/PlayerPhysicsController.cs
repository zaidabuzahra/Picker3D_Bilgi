using Controllers.Pool;
using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<PoolController> poolController = new List<PoolController>();
        [SerializeField] TextMeshProUGUI DiamondText;
        [SerializeField] TextMeshProUGUI WhiteBallCount;
        [SerializeField] TextMeshProUGUI RedBallCount;
        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Public&Private Variables

        private byte LevelID;
        public bool bChangeCountBalls = true;

        public int TotalWhiteCount;
        public int TotalRedCount;
        public int DifferenceTotal;
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StageArea"))
            {
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisbaleInput?.Invoke();
                DOVirtual.DelayedCall(3, () =>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>()
                        .TakeStageResult(manager.StageValue);
                    if (result)
                    {
                        if (bChangeCountBalls)
                        {
                            TotalWhiteCount += poolController[LevelID]._collectedCount;
                            poolController[LevelID]._collectedCount = 0;
                            WhiteBallCount.text = TotalWhiteCount.ToString();
                            LevelID++;
                        }

                        else
                        {
                            TotalRedCount += poolController[LevelID]._collectedCount;
                            CoreGameSignals.Instance.onCalculateDifference?.Invoke();
                            poolController[LevelID]._collectedCount = 0;
                            DiamondText.text = DifferenceTotal.ToString();
                            RedBallCount.text = TotalRedCount.ToString();
                            LevelID++;
                        }

                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                        InputSignals.Instance.onEnableInput?.Invoke();
                    }

                    else CoreGameSignals.Instance.onLevelFailed?.Invoke();
                });
                return;
            }

            if (other.CompareTag("Finish"))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                InputSignals.Instance.onDisbaleInput?.Invoke();
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                return;
            }

            if (other.CompareTag("MiniGame"))
            {
                //Write Mini Game Conditions
                CoreGameSignals.Instance.onSpeedPlayer?.Invoke();
                bChangeCountBalls = false;
                return;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var transform1 = manager.transform;
            var position = transform1.position;
            Gizmos.DrawSphere(new Vector3(position.x, position.y - 1.2f, position.z + 1f), 1.65f);
        }

        internal void OnReset()
        {
        }

        private void Start()
        {
            DiamondText = FindObjectsOfType<TextMeshProUGUI>()[2];
            WhiteBallCount = FindObjectsOfType<TextMeshProUGUI>()[3];
            RedBallCount = FindObjectsOfType<TextMeshProUGUI>()[4];
        }
    }
}