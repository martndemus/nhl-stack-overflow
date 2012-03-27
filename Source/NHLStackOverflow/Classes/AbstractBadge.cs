
namespace NHLStackOverflow.Classes
{
    public abstract class AbstractBadge
    {
        // Name, id, aantal van iets?
        // Methode die checkt of je aan deze badge voldoet
        public static string badgeName;
        public static string badgeDescription;
        public static int badgeID;
        public static int RequiredAmmount;
        public abstract bool badgeAchieve(int userID);
        public abstract void awardBadge(int userID);
    }
}  