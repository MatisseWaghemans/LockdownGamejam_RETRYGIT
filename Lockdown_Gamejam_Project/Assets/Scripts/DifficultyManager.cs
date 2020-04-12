using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Difficulty
{
    easy=0,medium=1,hard=2
}
public class DifficultyManager :MonoBehaviour
{
    void OnAwake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetDifficulty(Difficulty diff)
    {
        if(diff == Difficulty.easy)
        {
            Generator.NumberOfRooms =5;
            RoomTriggerScript.VoteFrequency =2;
            VoteBooth.removeAmount=2;
        }
        if(diff == Difficulty.medium)
        {
            Generator.NumberOfRooms =10;
            RoomTriggerScript.VoteFrequency =3;
            VoteBooth.removeAmount=5;
        }
        if(diff == Difficulty.hard)
        {
            Generator.NumberOfRooms =15;
            RoomTriggerScript.VoteFrequency =5;
            VoteBooth.removeAmount=10;
        }
    }
    public void OnClick(int difficulty)
    {
        if(difficulty==0)
        {
            SetDifficulty(Difficulty.easy);
        }
        if(difficulty==1)
        {
            SetDifficulty(Difficulty.medium);
        }
        if(difficulty==2)
        {
            SetDifficulty(Difficulty.hard);
        }
    }
}
