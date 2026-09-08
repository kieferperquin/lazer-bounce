using UnityEngine;

public class LaserSegment : MonoBehaviour
{
    public Material laserMaterial;
    public LaserSegment nextSegment;

    public void CreateNextSegment(MirrorScript mirror)
    {
        nextSegment = mirror.CreateNextSegment(laserMaterial);
    }

    public void CreateNewColorNextSegment(LaserColorChange colorChange)
    {
        nextSegment = colorChange.ColorChange();
    }

    public void UpdateNextSegment(GameObject nextSegmentObject, RaycastHit oldHit, Vector3 laserOrigin)
    {
        if (nextSegmentObject.gameObject.CompareTag("Mirror"))
        {
            nextSegmentObject.GetComponent<MirrorScript>().UpdateLaser(oldHit, laserOrigin, nextSegment);
        }
        else if (nextSegmentObject.gameObject.CompareTag("ColorChange"))
        {
            nextSegmentObject.GetComponent<LaserColorChange>().UpdateLaser(oldHit, laserOrigin, nextSegment);
        }
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
