using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    private double enemyHp = 50;
    public enum EnemyType
    {
        Normal,
        Archer,
    }
    public enum EnemyScale
    {
        Small,
        Mid,
        Big
    }
    private EnemyType enemyType = EnemyType.Normal;
    private EnemyScale enemyScale = EnemyScale.Small;
    public Sprite smallEnemy;
    public Sprite midEnemy;
    public Sprite bigEnemy;

    public void SetEnemyHp(double hp) {
        enemyHp = hp;
    }

    public void SetType(EnemyType type) {
        enemyType = type;
    }

    public void SetEnemySprite(string enemyType) {
        GetComponent<SpriteRenderer>().sprite = enemyType switch {
            "small" => smallEnemy,
            "mid" => midEnemy,
            "big" => bigEnemy,
            _ => smallEnemy // 默认使用小型敌人
        };
    }
    public void getHurt(double damage) {
        enemyHp -= damage;
        if (enemyHp <= 0) {
            DestroySelf();
        }
    }

    public void DestroySelf() {
        PlayerData.currentEnemyCount--;
        Destroy(gameObject);
    }
}
