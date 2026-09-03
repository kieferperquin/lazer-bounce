using UnityEngine;

public class LaserSegment : MonoBehaviour
{
    public Material laserMaterial;
    public LaserSegment nextSegment;

    public void CreateNextSegment(MirrorScript mirror)
    {
        nextSegment = mirror.CreateNextSegment(laserMaterial);
    }

    public void UpdateNextSegment(MirrorScript mirror, RaycastHit oldHit, Vector3 laserOrigin)
    {
        mirror.UpdateLaser(oldHit, laserOrigin, nextSegment);
    }

    public void DeleteNextSegment()
    {
        LaserSegment child = nextSegment;
        nextSegment = null;

        if (child != null)
        {
            child.DeleteNextSegment();

            Destroy(child.gameObject);
        }
    }
}
