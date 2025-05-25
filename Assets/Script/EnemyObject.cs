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
    public EnemyType enemyType = EnemyType.Normal;
    private EnemyScale enemyScale = EnemyScale.Small;
    public Sprite smallEnemy;
    public Sprite midEnemy;
    public Sprite bigEnemy;
    public Sprite smallEnemy2;
    public Sprite midEnemy2;
    public Sprite bigEnemy2;

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
        if(enemyType == EnemyType.Archer) {
            maxHp *= 2;
            currentHp *= 2;
        }
    }

    public void SetType(EnemyType type) {
        enemyType = type;
    }

    public void SetEnemySprite(string Type) {
        if(enemyType == EnemyType.Archer) {
            GetComponent<SpriteRenderer>().sprite = Type switch {
                "small" => smallEnemy2,
                "mid" => midEnemy2,
                "big" => bigEnemy2,
                _ => smallEnemy // 默认使用小型敌人
            };
        } else {
            GetComponent<SpriteRenderer>().sprite = Type switch {
                "small" => smallEnemy,
                "mid" => midEnemy,
                "big" => bigEnemy,
                _ => smallEnemy // 默认使用小型敌人
            };
        }
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