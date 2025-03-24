using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    [SerializeField] private GameObject _player;
    [SerializeField] private List<AIBase> _ai = new();

    public void RegisterPlayer(GameObject InPlayer)
    {
        _player = InPlayer;
    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    public void RegisterAI(AIBase inAI)
    {
        _ai.Add(inAI);
    }

    public void RemoveAI(AIBase inAI)
    {
        _ai.Remove(inAI);
    }

    public List<AIBase> GetAI()
    {
        return _ai;
    }
}