using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour {
    public GameObject EggPrefab;
    int m_width = 10;
    int m_height = 5;
    float m_x_offset = 1.25f;
    float m_y_offset = 1.15f;

    public Material[] material_array;

	// Use this for initialization
	void Start () {
        Random rand = new Random();
		for (int i = 0; i < m_height; i++) {
            for (int j = 0; j < m_width; j++) {
                GameObject obj = Instantiate(EggPrefab) as GameObject;
                obj.transform.SetParent(this.transform);
                obj.transform.localPosition = new Vector3((j + 0.5f - m_width / 2.0f) * m_x_offset, i * m_y_offset, 0.0f);

                int randNum = Random.Range(0, material_array.Length);
                obj.GetComponent<MeshRenderer>().material = material_array[randNum];
            }
        }
        SceneController.Level_Current.GetComponent<Bomb_ScoreCal>().TotalEgg = m_width * m_height;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
