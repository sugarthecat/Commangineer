using Commangineer.Units;

namespace Commangineer.Auuki
{
    /// <summary>
    /// A target towards a Auuki
    /// </summary>
    public interface AuukiTarget : Target
    {
        /// <summary>
        /// Damages the Auuki from a attacker
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        /// <param name="damageDealer">The attacker</param>
        public void Damage(int damage, Unit damageDealer);
    }
}