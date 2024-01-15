namespace Commangineer
{
    /// <summary>
    /// Represents a textured object that can be rotated
    /// </summary>
    public interface RotatableTexturedObject : TexturedObject
    {
        public float Angle { get; }
    }
}