using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script to define field of view
/// Attaching this script to a game object can give the object a viewcone.
/// In this viewcone, players and enemies will not block the viewcone, but the walls will.
/// 
/// In order to have enemies and players not blocking the viewcone:
///     This script requires all objects that are supposed to block the view to be in the same layer (This layer is set as "Walls" for right now)
///         The layer needs to be set as the layer with all view blocking objects as the layerMask variable in the FieldOfView
///     Layer for the view blocking objects needed to be added as a new layer
///         This can be set through the Inscpecter >> Layer >> Add Layer...
///     
/// In order to have walls blocking the viewcone:
///     Colliding objects have to have a BoxCollider2D
///     
/// Appearance of the view cone:
///     Create a new material in the project view
///     Choose "sprite" as the shader of the material
///     Set up color and the alpha (for transparency) value for the material in the inspector view
/// 
/// Variable(s)
///     int ViewConeSize - set the length of the ray of the viewcone
///     float fov - filed of view - set the angle of the viewcone
///     
/// TODO: decide what layer to use for all view blocking objects
/// TODO: decide whether players/enemies should block views
/// TODO: decide fov and viewcone size for the viewcone
/// </summary>
/// <author>Yingren Wang</author>

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; // used for not colliding with enemies

    // Hidden Components
    private Mesh mesh;

    // Public Elements
    // TODO: decide what should be private and what should not
    public float fov;// define the angle for field of view
    public Vector3 origin;
    public int rayCount; // the more rays we have the finer the view cone would be
    public float currentAngle;
    public float startingAngle;

    public float viewConeSize;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 90f; // define the angle for field of view
    }

    // Update is called once per frame
    void Update()
    {
        viewConeSize = 5.0f;
        rayCount = 50; // the more rays we have the finer the view cone would be
        currentAngle = startingAngle;
        float angleIncrease = fov / rayCount;
        
        Vector3[] vertices = new Vector3[rayCount + 2]; // add two for ray zero and origin
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1; // since index 0 will be the origin, start at 1
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            layerMask = LayerMask.GetMask("Walls");
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(currentAngle), viewConeSize, layerMask);

            // detect collision
            if (raycastHit2D.collider == null)
            {
                // not hitting
                vertex = origin + GetVectorFromAngle(currentAngle) * viewConeSize;
            }
            else
            {
                print("collideing");
                // hit with something
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                // triangle info
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;

            currentAngle -= angleIncrease;
        }

        // assign vertices and triangles
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    ///// <summary>
    ///// A function that will set the origin
    ///// </summary>
    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    ///// <summary>
    ///// A function that will set the aim direction
    ///// </summary>
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }

    ///// <summary>
    ///// Helper function for setting aim direction
    ///// A function that will return the angle of a vector
    ///// </summary>
    public static float GetAngleFromVectorFloat(Vector3 direction)
    {
        direction = direction.normalized;
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360; // convert to positive
        }
        return n;
    }

    /// <summary>
    /// Helper function for viewcone raycast
    /// A function that will return the vector of an angle.
    /// </summary>
    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    /// <summary>
    /// Generate a random direction
    /// </summary>
    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
}

