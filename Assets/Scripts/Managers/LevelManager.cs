using UnityEngine;
using Commands;
using Data.UnityObjects;
using Signals;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Region

        #region PublicVariables

        #endregion

        #region SerialzedVariables

        [SerializeField] private int totalLevelCount, levelID;
        [SerializeField] private Transform levelHolder;

        #endregion

        #region PrivateVariables

        private CD_Level _levelData;
        private OnLevelLoaderCommand _levelLoaderCommand;
        private OnLevelDestroyerCommand _levelDestroyerCommand;
        #endregion
        #endregion

        private void Awake()
        {
            _levelData = GetLevelData();
            levelID = GetActiveLevel();

            Init();
        }

        private int GetActiveLevel()
        {
            if (ES3.FileExists())
            {
                if (ES3.KeyExists("Level"))
                {
                    return ES3.Load<int>(key:"Level");
                }
            }

            return 0;
        }

        private CD_Level GetLevelData() => Resources.Load<CD_Level>(path: "Data/CD_Level");

        private void Start()
        {
            _levelLoaderCommand.Execute(levelID);
        }
        private void OnNextLevel()
        {
            levelID++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(levelID);
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(levelID);
        }

        private void Init()
        {
            _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
            _levelDestroyerCommand = new OnLevelDestroyerCommand(levelHolder);
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }
        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private void UnSubscribeEvenets()
        {
            CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnSubscribeEvenets();
        }
    }
}