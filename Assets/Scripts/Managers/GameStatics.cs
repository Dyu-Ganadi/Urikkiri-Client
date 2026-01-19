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
        public static GameFlowState State = GameFlowState.CARD_SELECTION;

        public static bool IsExaminer()
        {
            return Users.Any(participantInfo => participantInfo.isExaminer && participantInfo.userId == MyUserId);
        }

        public static void ResetExaminer()
        {
            foreach (var participantInfo in Users) participantInfo.isExaminer = false;
        }

        public static bool IsSelfUser(this ParticipantInfo user)
        {
            return user.userId == MyUserId;
        }

        public static ParticipantInfo GetParticipantInfo(long userId)
        {
            return Users.FirstOrDefault(participantInfo => participantInfo.userId == userId);
        }
    }

    public enum GameFlowState
    {
        CARD_SELECTION,
        EXAMINER_SELECTION,
    }
}