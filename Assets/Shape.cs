using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Mesh myMesh;
    protected void Start()
    {
        var vertices2D = GameManager.Instance.GetCurrentVertices();

        var vertices3D = System.Array.ConvertAll<Vector2, Vector3>(vertices2D, v => v);

        // Use the triangulator to get indices for creating triangles
        var triangulator = new Triangulator(vertices2D);
        var indices = triangulator.Triangulate();

        // Generate a color for each vertex
        var colors = Enumerable.Range(0, vertices3D.Length)
                 .Select(i => Color.black)
                 .ToArray();

        // Create the mesh
        var mesh = new Mesh
        {
            vertices = vertices3D,
            triangles = indices,
            colors = colors
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        var co = gameObject.AddComponent<MeshCollider>();
        co.sharedMesh = mesh;
        // Set up game object with mesh;
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));

        var filter = gameObject.AddComponent<MeshFilter>();
        filter.mesh = mesh;

        this.myMesh = mesh;
    }

    public void ChangeColor()
    {
        var c = Random.ColorHSV();
        //this.GetComponent<MeshRenderer>().material.SetColor("_Color", c);

        var colors = Enumerable.Range(0, this.myMesh.vertices.Length)
            .Select(i => c)
            .ToArray();

        this.myMesh.colors = colors;


    }

    public void ChangeShape(int id)
    {
        GameManager.Instance.ChangeCurrentShape(id);

        var vertices2D = GameManager.Instance.GetCurrentVertices();

        var vertices3D = System.Array.ConvertAll<Vector2, Vector3>(vertices2D, v => v);

        // Use the triangulator to get indices for creating triangles
        var triangulator = new Triangulator(vertices2D);
        var indices = triangulator.Triangulate();
        var colors = Enumerable.Range(0, vertices3D.Length)
         .Select(i => this.myMesh.colors[0])
         .ToArray();

        var mesh = new Mesh
        {
            vertices = vertices3D,
            triangles = indices,
            colors = colors
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        this.GetComponent<MeshCollider>().sharedMesh = mesh;
        var filter = gameObject.GetComponent<MeshFilter>();
        filter.mesh = mesh;
        this.myMesh = mesh;

    }

    protected bool GetTouchPoint(out Vector3 point)
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            point = Input.GetTouch(0).position;
            return true;
        }

        if(Input.GetMouseButtonDown(0))
        {
            point= Input.mousePosition;
            return true;
        }
        point = Vector3.zero;
        return false;


    }
    protected void Update()
    {
        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    this.ChangeColor();
        //}



        Vector3 touchPoint = new Vector3();
        if (!this.GetTouchPoint(out touchPoint))
            return;

        this.clicked = true;
        Ray ray = Camera.main.ScreenPointToRay(touchPoint);
        this.myRay = ray;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray.origin, ray.direction * 10000f, out hit))
        {
            this.ChangeColor();
        }

    }

    private Ray myRay;
    private bool clicked;
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        if (!this.clicked)
            return;
        Gizmos.color = Color.red;
        
        Gizmos.DrawRay(this.myRay.origin,this.myRay.direction * 10000f);
    }

}
