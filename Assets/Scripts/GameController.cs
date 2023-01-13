using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<GameObject> enemies;

    private void Awake()
    {
        enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        foreach (GameObject enemy in enemies.ToArray())
        {
            if (enemy.GetComponent<DeathController>().killed)
            {
                enemies.Remove(enemy);
            }
        }
        
        if (enemies.Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
