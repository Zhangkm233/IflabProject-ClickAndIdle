using System;

public static class PlayerData
{
    //��̬��洢��Ϸ����ҵ����ݣ��л�����ʱ���ᶪʧ����������̵�֮���������һ�����������������������ߵ�����
    public static double PlayerCoin;
    public static double PlayerHp;

    public static int currentEnemyCount; //��ǰ��������
    public enum GameState
    {
        Playing,
        Shopping,
        Pause
    }
    public static GameState CurrentGameState = GameState.Shopping; 
}
