using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("生成设置")]
    public GameObject enemyPrefab;
    public float spawnRate = 0.3f;
    public float spawnAreaWidth = 10f;
    public float spawnHeight = 5f;
    public int maxEnemies = 20;

    [Header("敌人设置")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float minScale = 0.2f;
    public float maxScale = 0.5f;
    public float originHp = 150f;
    public float originSpeed = 3f;

    void Start() {
        
    }

    public void StartSpawn() {
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies() {
        while (true) {
            if (PlayerData.CurrentGameState == PlayerData.GameState.Playing) {
                if (PlayerData.currentEnemyCount < maxEnemies) {
                    SpawnEnemy();
                    yield return new WaitForSeconds(spawnRate);
                } else {
                    yield return null;
                }
            }
        }
    }

    void SpawnEnemy() {
        //随机敌人位置 大小 和血量
        float randomX = Random.Range(-spawnAreaWidth / 2f,spawnAreaWidth / 2f);
        Vector3 spawnPosition = new Vector3(randomX,spawnHeight,0f);
        GameObject enemy = Instantiate(enemyPrefab,spawnPosition,Quaternion.identity);
        PlayerData.currentEnemyCount++;

        float randomNum = Random.Range(minScale,maxScale);
        enemy.transform.localScale = new Vector3(randomNum,randomNum,1f);

        //随机敌人类别
        int randomType = Random.Range(0,10);
        switch (randomType) {
            case < 2:
                enemy.GetComponent<EnemyObject>().SetType(EnemyObject.EnemyType.Archer);
                EnemyMovement movement = enemy.AddComponent<EnemyMovement>();
                movement.SetSpeed(originSpeed - originSpeed * (randomNum / maxScale) + 0.5f);
                break;
            case >= 2:
                enemy.GetComponent<EnemyObject>().SetType(EnemyObject.EnemyType.Normal);
                //设置速度
                movement = enemy.AddComponent<EnemyMovement>();
                movement.SetSpeed(originSpeed - originSpeed * (randomNum / maxScale) + 0.5f);
                break;
        }

        enemy.GetComponent<EnemyObject>().SetEnemyHp(originHp * randomNum);

        if (randomNum < 0.3f) {
            //对应大小设置sprite
            enemy.GetComponent<EnemyObject>().SetEnemySprite("small");
        } else if (randomNum < 0.4f) {
            enemy.GetComponent<EnemyObject>().SetEnemySprite("mid");
        } else {
            enemy.GetComponent<EnemyObject>().SetEnemySprite("big");
        }
        
    }
}