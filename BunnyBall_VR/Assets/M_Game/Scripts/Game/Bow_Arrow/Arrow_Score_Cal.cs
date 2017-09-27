using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Score_Cal : ScoreCalculation {
    private int m_arrow_total;
    private int m_arrow_target;
    private int[] m_score = new int[6];

    public void Arrow_Target(int _score) {
        m_arrow_target++;
        m_score[_score]++;
    }

    public override void Calculate() {
        int scoreTotal = 0;
        for (int i = 1; i < 6; i++)
            scoreTotal += i * m_score[i];

        int[] scores = new int[] { m_arrow_target, m_bulletFired_num,  scoreTotal};
        string[] names = new string[] { "On", "Total", "Score" };

        this.Calculate(scores, names);
    }
}