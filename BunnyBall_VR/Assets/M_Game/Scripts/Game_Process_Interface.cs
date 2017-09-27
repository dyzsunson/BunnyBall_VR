using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Game_Process_Interface {
    void GameReady();
    void GameStart();
    void GameEnd();
    void GameEndBuffer();
}
