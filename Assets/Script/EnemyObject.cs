using UnityEngine;
using UnityEngine.UI;

public class EnemyObject : MonoBehaviour
{
    private double maxHp = 50;
    private double currentHp = 50;
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

    public Image HPBAR;

    void Update()
    {
        float percent = - (float)(0.585 * (1-(currentHp / maxHp)));
        RectTransform rt = HPBAR.GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(percent, rt.offsetMax.y);
    }

    public void SetEnemyHp(double hp)
    {
        maxHp = hp;
        currentHp = hp;
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
        currentHp -= damage;
        if (currentHp <= 0) {
            DestroySelf();
        }
    }

    public void DestroySelf() {
        PlayerData.currentEnemyCount--;
        Destroy(gameObject);
    }
    
    public double GetCurrentHealth()
    {
        return currentHp;
    }
    
    public double GetMaxHealth() {
        return maxHp;
    }
}