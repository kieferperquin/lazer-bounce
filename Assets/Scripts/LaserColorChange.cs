using UnityEngine;

public class LaserColorChange : MonoBehaviour
{
    [SerializeField] private GameObject Lens;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private Material newLaserColor;

    private void Start()
    {
        Lens.GetComponent<Renderer>().material.color = new Color(newLaserColor.color.r, newLaserColor.color.g, newLaserColor.color.b, 0.4f);
    }

    public LaserSegment ColorChange()
    {
        GameObject createdObject = Instantiate(laserObject);

        createdObject.transform.SetParent(transform, false);

        createdObject.GetComponent<LineRenderer>().material = newLaserColor;

        LaserSegment segment = createdObject.GetComponent<LaserSegment>();

        segment.laserMaterial = newLaserColor;

        return segment;
    }

    public void UpdateLaser(RaycastHit oldHit, Vector3 laserOrigin, LaserSegment segment)
    {
        LineRenderer lr = segment.gameObject.GetComponent<LineRenderer>();

        Vector3 hitPoint = oldHit.point;

        lr.SetPosition(0, hitPoint);

        Vector3 incomingDir = (hitPoint - laserOrigin).normalized;

        RaycastHit hit;

        Vector3 rayOrigin = hitPoint + incomingDir * 0.01f;

        if (Physics.Raycast(rayOrigin, incomingDir, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);

                if (hit.collider.CompareTag("Mirror"))
                {
                    if (segment.nextSegment == null)
                    {
                        segment.CreateNextSegment(hit.collider.GetComponent<MirrorScript>());
                    }

                    segment.UpdateNextSegment(hit.collider.gameObject, hit, hitPoint);
                }
                else if (hit.collider.CompareTag("ColorChange"))
                {
                    if (segment.nextSegment == null)
                    {
                        segment.CreateNewColorNextSegment(hit.collider.GetComponent<LaserColorChange>());
                    }

                    segment.UpdateNextSegment(hit.collider.gameObject, hit, hitPoint);
                }
                else if (hit.collider.CompareTag("LazerGoal"))
                {
                    hit.transform.gameObject.GetComponent<LaserGoal>().CheckGoal(segment.laserMaterial);

                    segment.DeleteNextSegment();
                }
                else
                {
                    segment.DeleteNextSegment();
                }
            }
        }
        else
        {
            lr.SetPosition(1, hitPoint + incomingDir * 5000);

            segment.DeleteNextSegment();
        }
    }
}
