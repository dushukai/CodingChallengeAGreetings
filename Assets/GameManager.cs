using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager :Singleton<GameManager> 
{
    // Start is called before the first frame update

    //public Vector2[] vertices2D;

    private ShapeType currentShape;
    public enum ShapeType { Hexagon, Triangle, IPolygon}
    private Dictionary<ShapeType, Vector2[]> ShapeList = new Dictionary<ShapeType, Vector2[]>();

    protected void Awake()
    {
        this.currentShape = ShapeType.Hexagon;
        this.ShapeList[ShapeType.IPolygon] = new Vector2[] {
            new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,2),
            new Vector2(0,2),
            new Vector2(0,3),
            new Vector2(3,3),
            new Vector2(3,2),
            new Vector2(2,2),
            new Vector2(2,1),
            new Vector2(3,1),
            new Vector2(3,0),
        };
        // add more shape
        this.ShapeList[ShapeType.Hexagon] = new Vector2[] {
            new Vector2(-1f,0),
            new Vector2(-0.5f,0.866f),
            new Vector2(0.5f,0.866f),
            new Vector2(1f,0),
            new Vector2(0.5f,-0.866f),
            new Vector2(-0.5f,-0.866f)
           
        };

        this.ShapeList[ShapeType.Triangle] = new Vector2[] {
            new Vector2(-2f,0),
            new Vector2(2f,0),
            new Vector2(0f,1.5f),

        };

    }

    public Vector2[] GetCurrentVertices()
    {
        return this.ShapeList[this.currentShape];
    }

    public void ChangeCurrentShape(int id)
    {
       
        this.currentShape = (ShapeType)id;
    }


}
