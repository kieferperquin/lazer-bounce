using UnityEngine;

public class LaserGoal : MonoBehaviour
{
    [SerializeField] private Material laserGoalMat;
    
    public void CheckGoal(Material RayMat)
    {
        if (laserGoalMat == RayMat)
        {
            Debug.Log("goal");
        }
    }
}
