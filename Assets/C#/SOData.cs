using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SOData", order = 1)]
public class SOData : ScriptableObject
{
    public int _randomNumber;
    public int[] _PosID;
    public int _count;
    public int _stateOfTheGame;
    public Transform[] _possitions;
    public int _enemyStateDog;
    public int _enemySpriteDog;
    public int _enemyStateFarmer;
    public int _enemySpriteFarmer;
    public bool _InView = false;
}