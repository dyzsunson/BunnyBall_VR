using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MRandom : MonoBehaviour {
    int[] m_number_array;
    int m_current_num = -1;

    public static MRandom context;

    public void ResetFile(string _fileName) {
        if (!File.Exists(_fileName)) {
            return;
        }

        m_current_num = -1;
        string[] lines = File.ReadAllLines(_fileName);
        m_number_array = new int[lines.Length];

        for (int i = 0; i < m_number_array.Length; i++) {
            m_number_array[i] = int.Parse(lines[i]);
        }
    }

    void BuildFile(int _length, string _fileName) {
        string[] temp_array = new string[_length];
        for (int i = 0; i < _length; i++) {
            temp_array[i] = Random.Range(0, 1001).ToString();
        }

        if (!File.Exists(_fileName)) {
            File.Create(_fileName).Dispose(); ;
        }

        File.WriteAllLines(_fileName, temp_array);
    }

    private void Awake() {
        context = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K)) {
            BuildFile(100, "random_data.txt");
        }
	}

    public float GetNextRandom(float _min, float _max, bool _fix) {
        if (!_fix)
            return Random.Range(_min, _max);

        if (m_number_array == null || m_number_array.Length == 0)
            return -1.0f;

        m_current_num = (m_current_num + 1) % m_number_array.Length;

        return _min + (_max - _min) * m_number_array[m_current_num] / 1000.0f;
    }

    public int GetNextRandom(int _min, int _max, bool _fix) {
        if (!_fix)
            return Random.Range(_min, _max);

        if (m_number_array == null || m_number_array.Length == 0)
            return -1;

        m_current_num = (m_current_num + 1) % m_number_array.Length;

        int num = (int)(_min + (_max - _min) * m_number_array[m_current_num] / 1000.0f);
        if (num == _max)
            num -= 1;

        return num;
    }
}
