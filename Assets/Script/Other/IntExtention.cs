using UnityEngine;

public static class IntExtention
{
    public static int LoopedClamp(int i, int min, int max)
    {
        if(i < min) return max;
        if(i > max) return min;
        return i;
    }
    

}
