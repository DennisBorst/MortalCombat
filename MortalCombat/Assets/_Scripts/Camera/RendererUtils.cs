using System.Collections.Generic;
using UnityEngine;

public static class RendererUtils
{
    public static List<Renderer> GetAllRenderers(List<GameObject> objects) 
    {
        List<Renderer> renderers = new List<Renderer>();
        for (int i = 0; i < objects.Count; i++)
        {
            renderers.AddRange(objects[i].GetComponentsInChildren<Renderer>());
        }
        
        return renderers;
    }

    public static Rect Get2DBoundingRect(List<GameObject> objects)
        => Get2DBoundingRect(GetAllRenderers(objects));

    public static Rect Get2DBoundingRect(List<Renderer> renderers)
    {
        Bounds bounds = new Bounds();

        if (renderers.Count >= 1)
        {
            bounds = renderers[0].bounds;
        }

        for (int i = 1; i < renderers.Count; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        Vector2 size = new Vector2(bounds.size.x, bounds.size.y);
        Vector2 position = new Vector2(bounds.center.x, bounds.center.y) - (size * 0.5f);

        return new Rect(position, size);
    }
}