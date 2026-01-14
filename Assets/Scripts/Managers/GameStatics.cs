using System.Linq;
using Network;

namespace Managers
{
    public static class GameStatics
    {
        public static long MyUserId;
        public static string RoomCode;
        public static QuizResponse Question;
        public static CardListResponse CardList;
        public static ParticipantInfo[] Users;

        public static bool IsExaminer()
        {
            return Users.Any(participantInfo => participantInfo.isExaminer && participantInfo.userId == MyUserId);
        }
    }
}