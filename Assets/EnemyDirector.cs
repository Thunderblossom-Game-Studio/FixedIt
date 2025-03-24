using System;
using UnityEngine;
using System.Linq;

public class EnemyPointer : MonoBehaviour
{
    private Transform _player; // Reference to the player's transform

    [SerializeField] private Transform arrowModel; // Assign the 3D model representing the arrow
    [SerializeField] private float detectionRadius = 20f;
    [SerializeField] private float offsetDistance = 2f; // Offset distance from the player
    [SerializeField] private float disableDistance = 5f; // Distance at which arrow should be disabled
    
    private void Awake()
    {
        _player = transform;
    }

    void Update()
    {
        Transform closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(_player.position, closestEnemy.position);
            
            if (distanceToEnemy < disableDistance)
            {
                arrowModel.gameObject.SetActive(false);
                return;
            }
            
            Vector3 directionToEnemy = (closestEnemy.position - _player.position).normalized;
            float angle = Mathf.Atan2(directionToEnemy.z, directionToEnemy.x) * Mathf.Rad2Deg;
            arrowModel.rotation = Quaternion.LookRotation(directionToEnemy, Vector3.up); // Rotate to face enemy
            arrowModel.position = _player.position + directionToEnemy * offsetDistance; // Apply offset
            arrowModel.gameObject.SetActive(true);
        }
        else
        {
            arrowModel.gameObject.SetActive(false);
        }
    }

    Transform FindClosestEnemy()
    {
        var enemies = GameManager.Instance.GetAI(); // Optimized method to retrieve AI references
        Transform closest = null;
        float minDistance = detectionRadius;
        
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(_player.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        
        return closest;
    }
}
