using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public GameObject LevelCompletedUI;
    private List<GameObject> enemies;

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
            LevelCompletedUI.SetActive(true);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
