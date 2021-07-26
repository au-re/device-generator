using Godot;

static class PUtils
{
    // lerp a two Vector3
    public static Vector3 Lerp(Vector3 a, Vector3 b, float by)
    {
        float x = Mathf.Lerp(a.x, b.x, by);
        float y = Mathf.Lerp(a.y, b.y, by);
        float z = Mathf.Lerp(a.z, b.z, by);
        return new Vector3(x, y, z);
    }
}