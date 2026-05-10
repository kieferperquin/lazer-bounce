using UnityEngine;

public class LazerGoal : MonoBehaviour
{
    [SerializeField] private Material lazerGoalMat;
    
    public void CheckGoal(Material RayMat)
    {
        if (lazerGoalMat == RayMat)
        {
            Debug.Log("goal");
        }
    }
}
