using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculation : MonoBehaviour {
    // Score
    public Text ScoreText;
    public Text RecordText;

    public Text ScoreTextVR;
    public Text RecordTextVR;

    protected int m_bulletFired_num = 0;
    protected int m_bulletBlocked_num = 0;

    string m_levelName;

    public void BulletFired() {
        m_bulletFired_num++;
    }

    public void BulletBlocked() {
        m_bulletBlocked_num++;
    }

    // Use this for initialization
    void Start () {
        m_levelName = this.GetComponent<Level>().LevelName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Calculate() {

    }

    protected void Calculate(int[] _scoreArray, string[] _names) {
        string path_name = "Ellic's Exercise Class_Data/Record/";
        string file_name = m_levelName + "_score.txt";
  
        if (!Directory.Exists(path_name)) {
            Directory.CreateDirectory(path_name);
        }

        bool isNew = false;
        string fileTxt = "";

        if (!File.Exists(path_name + file_name)) {
            File.Create(path_name + file_name).Dispose(); ;
            isNew = true;
        }

        string[] lines = File.ReadAllLines(path_name + file_name);
        string todayStr = DateTime.Today.ToString("yyyyMMdd");

        if (lines.Length == 0)
            isNew = true;

        if (!isNew) {
            if (lines[0] != todayStr)
                isNew = true;
        }

        string recordStr = "";

        if (isNew)
            fileTxt += todayStr;
        else
            fileTxt += lines[0];
        fileTxt += "\r\n";

        for (int i = 0; i < _scoreArray.Length; i++) {
            if (isNew || _scoreArray[i] > int.Parse(lines[i + 1])) {
                fileTxt += _scoreArray[i];
                recordStr += "NEW RECORD!";
                RecordText.transform.Find("Star" + i).gameObject.SetActive(true);
                RecordTextVR.transform.Find("Star" + i).gameObject.SetActive(true);
                
            }
            else {
                fileTxt += lines[i + 1];
                recordStr += lines[i + 1];

                RecordText.transform.Find("Dark" + i).gameObject.SetActive(true);
                RecordTextVR.transform.Find("Dark" + i).gameObject.SetActive(true);
            }
            fileTxt += "\r\n";
            recordStr += "\r\n";
        }

        File.WriteAllText(path_name + file_name, fileTxt);

        ScoreTextVR.text = ScoreText.text = "";
        for (int i = 0; i < _scoreArray.Length; i++) {
            ScoreText.text += _names[i] + ": " + _scoreArray[i] + "\r\n";
        }
        ScoreTextVR.text = ScoreText.text;

        RecordText.text = RecordTextVR.text = recordStr;
    }
}
