using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data")]
public class GameData : ScriptableObject
{

    public bool mouse = true;
    public bool AI = true;

    public int goal = 10;

}
