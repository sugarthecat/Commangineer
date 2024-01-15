using Commangineer.Units;

namespace Commangineer.Auuki
{
    public interface AuukiTarget : Target
    {
        public void Damage(int damage, Unit damageDealer);
    }
}