using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soccer_ScoreCal : ScoreCalculation {
    private int m_ballIn = 0;

    public void BallIn() {
        m_ballIn++;
    }

    public override void Calculate() {
        int[] scores = new int[] { m_ballIn, m_bulletFired_num, m_bulletBlocked_num };
        string[] names = new string[] { "Goal", "Balls Kicked", "Balls Blocked" };

        this.Calculate(scores, names);
    }
}
