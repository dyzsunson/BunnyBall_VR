using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_Score_Cal : ScoreCalculation {
    private int m_score = 0;
    private int m_in = 0;

    public void InBasket(bool _correct) {
        m_in++;
        if (_correct)
            m_score += 2;
        else
            m_score++;
    }

    public override void Calculate() {
        int[] scores = new int[] { m_score, m_in, m_bulletFired_num};
        string[] names = new string[] { "Scores", "In", "Total" };

        this.Calculate(scores, names);
    }
}
