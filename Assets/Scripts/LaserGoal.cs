using UnityEngine;

public class LaserGoal : MonoBehaviour
{
    [SerializeField] private GameObject colorIndicator;

    [SerializeField] private Material laserGoalMat;

    private void Start()
    {
        colorIndicator.GetComponent<Renderer>().material = laserGoalMat;
    }

    public void CheckGoal(Material RayMat)
    {
        if (laserGoalMat == RayMat)
        {
            Debug.Log("goal");
        }
    }
}
