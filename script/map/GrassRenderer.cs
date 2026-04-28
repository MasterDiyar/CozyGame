using Godot;
using System;
using System.Collections.Generic;


public partial class GrassRenderer : Node2D
{
    [Export] public Texture2D GrassTexture;
    [Export] public int Count = 10000; 
    [Export] public Vector2 AreaSize = new Vector2(1000, 1000);

    private readonly List<Rid> _instances = new List<Rid>();

    public override void _Ready()
    {
        Rid meshRid = RenderingServer.MeshCreate();
        
        var vertices = new Vector3[] {
            new Vector3(-16, -32, 0), new Vector3(16, -32, 0),
            new Vector3(16, 0, 0), new Vector3(-16, 0, 0)
        };
        var uvs = new Vector2[] {
            new Vector2(0, 0), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(0, 1)
        };
        var indices = new int[] { 0, 1, 2, 0, 2, 3 };

        var arrays = new Godot.Collections.Array();
        arrays.Resize((int)Mesh.ArrayType.Max);
        arrays[(int)Mesh.ArrayType.Vertex] = vertices;
        arrays[(int)Mesh.ArrayType.TexUV] = uvs;
        arrays[(int)Mesh.ArrayType.Index] = indices;

        RenderingServer.MeshAddSurfaceFromArrays(meshRid, RenderingServer.PrimitiveType.Triangles, arrays);
        var shader = GD.Load<Shader>("res://shader/grass.gdshader");
        Rid shaderRid = shader.GetRid();

        Rid materialRid = RenderingServer.MaterialCreate();
        RenderingServer.MaterialSetShader(materialRid, shaderRid);
        RenderingServer.MaterialSetParam(materialRid, "amplitude", 15.0f);

        Rid texRid = GrassTexture.GetRid();
        Rid canvasRid = GetCanvas();

        for (int i = 0; i < Count; i++)
        {
            Rid ciRid = RenderingServer.MultimeshCreate();
            RenderingServer.CanvasItemSetParent(ciRid, canvasRid);
            RenderingServer.CanvasItemSetZIndex(ciRid, GetZIndex());

            RenderingServer.MaterialSetParam(materialRid, "amplitude", 15.0f);

            RenderingServer.CanvasItemSetMaterial(ciRid, materialRid);
            
            RenderingServer.CanvasItemAddMesh(ciRid, meshRid, default, default, GrassTexture.GetRid());
            
            var pos = new Transform2D(0, new Vector2(
                GD.Randf() * AreaSize.X, 
                GD.Randf() * AreaSize.Y
            ));
            RenderingServer.CanvasItemSetTransform(ciRid, pos);
            
            _instances.Add(ciRid);
        }
    }

    public override void _ExitTree()
    {
        foreach (var rid in _instances)
            RenderingServer.FreeRid(rid);
        
    }
}
