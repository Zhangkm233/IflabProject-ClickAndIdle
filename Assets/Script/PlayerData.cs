using System;

public static class PlayerData
{
    //静态类存储游戏和玩家的数据，切换场景时不会丢失，所以如果商店之类的是另外一个场景可以用这个来管理这边的内容
    public static double PlayerCoin;
    public static double PlayerHp;

    public static int currentEnemyCount; //当前敌人数量
    public enum GameState
    {
        Playing,
        Shopping,
        Pause
    }
    public static GameState CurrentGameState = GameState.Shopping;

    public static int HpUpLevel = 0;
}
