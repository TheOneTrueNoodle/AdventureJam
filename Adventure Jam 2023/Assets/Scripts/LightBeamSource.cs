using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamSource : MonoBehaviour
{
    private bool lightsActive;
    private LightBeam lightBeam;
    public Vector2 StartDir;
    public Material lightBeamMaterial;
    public LayerMask lightBeamLayerMask;

    private void Update()
    {
        if(lightBeam == null)
        {
            CreateBeam();
        }
    }

    private void CreateBeam()
    {
        lightBeam = new LightBeam(transform.position, StartDir, 0.2f, 0.2f, Color.white, Color.white, lightBeamMaterial, lightBeamLayerMask);
    }
}

public class LightBeam
{
    LayerMask layerMask;
    public GameObject beamObj;
    private LineRenderer beam;
    private List<Vector2> beamIndices = new List<Vector2>();

    public LightBeam (Vector2 pos, Vector2 dir, float startWidth, float endWidth, Color startColor, Color endColor, Material material, LayerMask layerMask)
    {
        this.layerMask = layerMask;
        beam = new LineRenderer();
        beamObj = new GameObject();
        beamObj.name = "Light Beam";

        beam = this.beamObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        beam.startWidth = startWidth;
        beam.endWidth = endWidth;
        beam.startColor = startColor;
        beam.endColor = endColor;
        beam.material = material;

        CastBeam(pos, dir);
    }

    private void CastBeam(Vector2 pos, Vector2 dir)
    {
        beamIndices.Add(pos);

        Ray2D ray = new Ray2D(pos, dir);
        RaycastHit2D hit;

        if(hit = Physics2D.Raycast(pos, dir, 100, layerMask))
        {
            CheckHit(hit, dir);
        }
        else
        {
            beamIndices.Add(ray.GetPoint(100));
            UpdateBeam();
        }
    }

    private void CheckHit(RaycastHit2D hit, Vector2 direction)
    {
        if(hit.collider.tag.Equals("Mirror"))
        {
            Vector2 pos = hit.point;
            Vector2 dir = Vector2.Reflect(direction, hit.normal);

            CastBeam(pos, dir);
        }
        else
        {
            beamIndices.Add(hit.point);
            UpdateBeam();
        }
    }

    private void UpdateBeam()
    {
        int count = 0;
        beam.positionCount = beamIndices.Count;

        foreach(Vector2 index in beamIndices)
        {
            beam.SetPosition(count, index);
            count++;
        }
    }
}
