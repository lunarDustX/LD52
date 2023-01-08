using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    [SerializeField] Transform target;
    Mesh mesh;

    float fov = 90f;
    float viewDistance = 3f;

    private Vector3 origin;
    private float startingAngle;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    #region util
    Vector3 GetVectorFromAngle(float _angle)
    {
        float angleRad = _angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    float GetAngleFromVector(Vector3 _dir)
    {
        _dir = _dir.normalized;
        float n = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return n;
    }
    #endregion

    //private void Update()
    //{
    //    if (target != null)
    //    {
    //        SetOrigin(target.position);
    //        SetAimDirection(-target.right);
    //    }
    //}

    void LateUpdate()
    {
        int rayCount = 10;
        float angle = startingAngle;
        float angleIncrease = fov / (rayCount - 1);

        Vector3[] vertices = new Vector3[rayCount + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[(rayCount - 1) * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 dir = GetVectorFromAngle(angle);
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, dir, viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + dir * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 _origin)
    {
        this.origin = _origin;
    }

    public void SetAimDirection(Vector3 _aimDir)
    {
        this.startingAngle = GetAngleFromVector(_aimDir) + fov / 2f;
    }

    public void SetFov(float _fov)
    {
        this.fov = _fov;
    }

    public void SetViewDistance(float _viewDistance)
    {
        this.viewDistance = _viewDistance;
    }

}
