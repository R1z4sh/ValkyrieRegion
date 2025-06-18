using System.Runtime.CompilerServices;
using UnityEngine;

public class StatusCalcurion {  
   static public int calcCommonItem(int min,int max,int maxRate,int rate)
    {
        float lvValue = (max - min) / (maxRate - 1);
        return (int )(Mathf.Round(min + lvValue * (rate - min)));
    }
}
