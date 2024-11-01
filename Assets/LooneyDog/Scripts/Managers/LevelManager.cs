using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class LevelManager : MonoBehaviour
    {

        public int LevelNumber { get => _levelNumber; set => _levelNumber = value; }
        public GameDifficulty Difficulty { get => _difficulty; set => _difficulty = value; }
        
        [Header("Level Details")]
        [SerializeField] private int _levelNumber;
        [SerializeField] private GameDifficulty _difficulty;
        [SerializeField] private LevelData[] levelDatas;
        private LevelDataStruct Currentleveldata;

        public void SetCurrentLevelDetails(int levelnumber, GameDifficulty difficulty) //Called From LoadingScreen 
        {
            LevelNumber = levelnumber;
            Difficulty = difficulty;
        }

        public void GetLevelData(int levelNumber, GameDifficulty gamedifficulty)
        {
            if ((levelNumber - 1) == levelDatas[levelNumber - 2].LevelNumber)//-2 coz sciptable object array starts from 0
            {
                Currentleveldata = levelDatas[levelNumber - 2].GetLevelData(gamedifficulty);//-2 coz sciptable object array starts from 0
            }
            else
            {
                Debug.Log("LevelNumber Mismatch" + " The level number should be" + levelNumber + " but it is :=" + levelDatas[levelNumber - 2].LevelNumber);
            }
        }
    }
}
