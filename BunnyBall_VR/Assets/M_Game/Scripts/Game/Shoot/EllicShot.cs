using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllicShot : EllicControlller {
    public EllicTarget TargetPrefab;
    public EllicTarget BossPrefab;

    Transform[] m_targetPosition_array;
    Transform m_hide_position;
    EllicTarget[] m_target_array;
    int m_num = 0;

    protected override void Start() {
        // base.Start();
        m_hide_position = this.transform.Find("Hide_Position");
        m_targetPosition_array = new Transform[m_hide_position.childCount];
        m_target_array = new EllicTarget[m_hide_position.childCount];

        for (int i = 0; i < m_hide_position.childCount; i++)
            m_targetPosition_array[i] = m_hide_position.GetChild(i);
    }

    protected override void Update() {
        // base.Update();
        if (this.g_is_running && InputCtrl.IsPowerButtonUp)
            Create_Target();
    }

    public EllicTarget Create_Target() {
        int num = 0;

        bool has_empty = false;
        foreach (EllicTarget target in m_target_array) {
            if (target == null) {
                has_empty = true;
                break;
            }
        }

        if (has_empty == false)
            return null;

        do {
            num = Random.Range(0, m_targetPosition_array.Length);
        } while (m_target_array[num] != null);

        m_num++;
        if (m_num % 5 == 0)
            return Create_Target(num, BossPrefab);
        else
            return Create_Target(num, TargetPrefab);
    }

    public EllicTarget Create_Target(int _position, EllicTarget _prefab) {
        EllicTarget target = Instantiate(_prefab);
        target.transform.position = m_targetPosition_array[_position].position;
        m_target_array[_position] = target;
        return target;
    }
}
