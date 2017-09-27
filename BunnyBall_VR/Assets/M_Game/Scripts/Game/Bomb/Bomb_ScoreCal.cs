using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_ScoreCal : ScoreCalculation {
    private int m_totalEgg;
    private int m_hitEgg = 0;

    public int TotalEgg {
        set {
            m_totalEgg = value;
            m_hitEgg = 0;
        }
    }

    public void EggBreak() {
        m_hitEgg++;
    }

    public override void Calculate() {
        int[] scores = new int[] { m_totalEgg - m_hitEgg, m_hitEgg, m_bulletFired_num, m_bulletBlocked_num};
        string[] names = new string[] { "Cubes Remaining", "Cubes Lost", "Balls Fired", "Balls Blocked" };

        this.Calculate(scores, names);
    }
}
