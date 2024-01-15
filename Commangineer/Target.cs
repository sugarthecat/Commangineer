using Microsoft.Xna.Framework;

namespace Commangineer
{
    /// <summary>
    /// A target for a Auuki or player unit
    /// </summary>
    public interface Target
    {
        /// <summary>
        /// Damages the target
        /// </summary>
        /// <param name="damage">The amount of damage to deal</param>
        public void Damage(int damage);

        /// <summary>
        /// Gets if the target is alive
        /// </summary>
        public bool Alive { get; }

        /// <summary>
        /// Gets the center position of the target
        /// </summary>
        public Vector2 CenterPosition { get; }
    }
}