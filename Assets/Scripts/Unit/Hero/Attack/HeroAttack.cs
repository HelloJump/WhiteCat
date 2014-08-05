using System.Collections;

public class HeroAttack 
{
    public int attackIdx;
    public float attackStartTime;
    public float attackWholeTime;

    public HeroAttack(int attackIdx, float attackStartTime, float attackWholeTime)
    {
        this.attackIdx = attackIdx;
        this.attackStartTime = attackStartTime;
        this.attackWholeTime = attackWholeTime;
    }
}
