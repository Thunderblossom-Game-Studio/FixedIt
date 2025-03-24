using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Helper script to register player with game manager
    void Start()
    {
        GameManager.Instance.RegisterPlayer(gameObject);
    }
}
