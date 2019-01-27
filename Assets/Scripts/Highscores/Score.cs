using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Score
{
    public string name;
    public long value;

    public Score(string name, long value)
    {
        this.name = name;
        this.value = value;
    }
}
