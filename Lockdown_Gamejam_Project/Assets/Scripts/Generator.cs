using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] SpawnRooms;
    public GameObject[] EndRooms;
    public GameObject[] HorizontalRooms;
    public GameObject[] VerticalRooms;
    public GameObject[] LWayRightTopRooms;
    public GameObject[] LWayRightBottomRooms;
    public GameObject[] LWayLeftTopRooms;
    public GameObject[] LWayLeftBottomRooms;

    public List<Vector2> _roomPositionList = new List<Vector2>();
    public readonly List<GameObject> _roomList = new List<GameObject>();

    [SerializeField]
    public int NumberOfRooms = 5;

    private float _offsetHorizontal = 28f;
    private float _offsetVertical = 16f;

    private Vector2 _position = Vector2.zero;
    private Vector2 _lastPosition = Vector2.zero;

    private GameObject _room;

    private int _randomRoomIndex = 0;
    public int CurrentRoom =0;

    private void Start()
    {
        GeneratePositions();
        AddRooms();
    }

    private void AddRooms()
    {
        Vector2 previousPosition = Vector2.zero;
        Vector2 nextPosition = _roomPositionList[1];
        Vector2 position = _roomPositionList[0];

        Vector2 previousPositionCalculation;
        Vector2 nextPositionCalculation;

        if (nextPosition == Vector2.left * _offsetHorizontal)
        {
            _room = Instantiate(SpawnRooms[3], position, Quaternion.identity);
        }
        else if (nextPosition == Vector2.right * _offsetHorizontal)
        {
            _room = Instantiate(SpawnRooms[1], position, Quaternion.identity);
        }
        else if (nextPosition == Vector2.up * _offsetVertical)
        {
            _room = Instantiate(SpawnRooms[0], position, Quaternion.identity);
        }
        else if (nextPosition == Vector2.down * _offsetVertical)
        {
            _room = Instantiate(SpawnRooms[2], position, Quaternion.identity);
        }

        _roomList.Add(_room);

        for (int positionIndex = 1; positionIndex < _roomPositionList.Count - 1; positionIndex++)
        {
            position = _roomPositionList[positionIndex];
            previousPosition = _roomPositionList[positionIndex - 1];
            nextPosition = _roomPositionList[positionIndex + 1];

            previousPositionCalculation = previousPosition - position;
            nextPositionCalculation = nextPosition - position;

            //links rechts
            if (previousPositionCalculation == Vector2.left * _offsetHorizontal && nextPositionCalculation == Vector2.right * _offsetHorizontal)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, HorizontalRooms.Length);
                _room = Instantiate(HorizontalRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            else if (previousPositionCalculation == Vector2.right * _offsetHorizontal && nextPositionCalculation == Vector2.left * _offsetHorizontal)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, HorizontalRooms.Length);
                _room = Instantiate(HorizontalRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            //boven onder
            else if (previousPositionCalculation == Vector2.up * _offsetVertical && nextPositionCalculation == Vector2.down * _offsetVertical)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, VerticalRooms.Length);
                _room = Instantiate(VerticalRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            else if (previousPositionCalculation == Vector2.down * _offsetVertical && nextPositionCalculation == Vector2.up * _offsetVertical)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, VerticalRooms.Length);
                _room = Instantiate(VerticalRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            //links boven
            else if (previousPositionCalculation == Vector2.left * _offsetHorizontal && nextPositionCalculation == Vector2.up * _offsetVertical)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayLeftTopRooms.Length);
                _room = Instantiate(LWayLeftTopRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            else if (previousPositionCalculation == Vector2.up * _offsetVertical && nextPositionCalculation == Vector2.left * _offsetHorizontal)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayLeftTopRooms.Length);
                _room = Instantiate(LWayLeftTopRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            //links onder
            else if (previousPositionCalculation == Vector2.left * _offsetHorizontal && nextPositionCalculation == Vector2.down * _offsetVertical)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayLeftBottomRooms.Length);
                _room = Instantiate(LWayLeftBottomRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            else if (previousPositionCalculation == Vector2.down * _offsetVertical && nextPositionCalculation == Vector2.left * _offsetHorizontal)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayLeftBottomRooms.Length);
                _room = Instantiate(LWayLeftBottomRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            //Rechts boven
            else if (previousPositionCalculation == Vector2.right * _offsetHorizontal && nextPositionCalculation == Vector2.up * _offsetVertical)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRightTopRooms.Length);
                _room = Instantiate(LWayRightTopRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            else if (previousPositionCalculation == Vector2.up * _offsetVertical && nextPositionCalculation == Vector2.right * _offsetHorizontal)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRightTopRooms.Length);
                _room = Instantiate(LWayRightTopRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            //Rechts onder
            else if (previousPositionCalculation == Vector2.right * _offsetHorizontal && nextPositionCalculation == Vector2.down * _offsetVertical)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRightBottomRooms.Length);
                _room = Instantiate(LWayRightBottomRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            else if (previousPositionCalculation == Vector2.down * _offsetVertical && nextPositionCalculation == Vector2.right * _offsetHorizontal)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRightBottomRooms.Length);
                _room = Instantiate(LWayRightBottomRooms[_randomRoomIndex], position, Quaternion.identity);
            }

            _roomList.Add(_room);
        }

        previousPosition = _roomPositionList[_roomPositionList.Count - 2];
        position = _roomPositionList[_roomPositionList.Count - 1];

        previousPositionCalculation = previousPosition - position;

        if (previousPositionCalculation == Vector2.left * _offsetHorizontal)
        {
            _room = Instantiate(EndRooms[3], position, Quaternion.identity);
        }
        else if (previousPositionCalculation == Vector2.right * _offsetHorizontal)
        {
            _room = Instantiate(EndRooms[1], position, Quaternion.identity);
        }
        else if (previousPositionCalculation == Vector2.up * _offsetVertical)
        {
            _room = Instantiate(EndRooms[0], position, Quaternion.identity);
        }
        else if (previousPositionCalculation == Vector2.down * _offsetVertical)
        {
            _room = Instantiate(EndRooms[2], position, Quaternion.identity);
        }

        _roomList.Add(_room);
    }

    private void GeneratePositions()
    {
        bool positionsGenerated = false;
        while (!positionsGenerated)
        {
            bool hasOverLap = false;
            _position = Vector3.zero;
            _lastPosition = Vector3.zero;
            //add the spawn position
            _roomPositionList.Add(_position);

            //add the other rooms position, including the end room
            for (int roomIndex = 0; roomIndex < NumberOfRooms; roomIndex++)
            {
                //do this calculation while the room position already exists
                int randomDirection = UnityEngine.Random.Range(0, 4);
                int counter = 0;
                do
                {
                    ++counter;
                    hasOverLap = false;
                    switch (randomDirection)
                    {
                        case 0:
                            _position = _lastPosition + Vector2.left * _offsetHorizontal;
                            if (IsOverlapping())
                            {
                                hasOverLap = true;
                                ++randomDirection;
                            }
                            break;
                        case 1:
                            _position = _lastPosition + Vector2.up * _offsetVertical;
                            if (IsOverlapping())
                            {
                                hasOverLap = true;
                                ++randomDirection;
                            }
                            break;
                        case 2:
                            _position = _lastPosition + Vector2.right * _offsetHorizontal;
                            if (IsOverlapping())
                            {
                                hasOverLap = true;
                                ++randomDirection;
                            }
                            break;
                        case 3:
                            _position = _lastPosition + Vector2.down * _offsetVertical;
                            if (IsOverlapping())
                            {
                                hasOverLap = true;
                                randomDirection = 0;
                            }
                            break;
                    }
                } while (hasOverLap && counter >= 4);

                if (hasOverLap)
                {
                    _roomPositionList.Clear();
                    break;
                }

                _roomPositionList.Add(_position);

                _lastPosition = _position;
            }
            if (!hasOverLap)
            {
                positionsGenerated = true;
            }
        }
    }
    bool IsOverlapping()
    {
        //Check if the roomposition already exists

        for (int positionIndex = 0; positionIndex < _roomPositionList.Count; positionIndex++)
        {
            if (_roomPositionList[positionIndex] == _position)
            {
                return true;
            }
        }
        return false;
    }
}
