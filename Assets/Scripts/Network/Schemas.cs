using System;
using System.Collections.Generic;
using GameLogic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.VisualScripting;
// ReSharper disable InconsistentNaming

namespace Network
{
    [Serializable]
    public class Void
    {
    }

    [Serializable]
    public class GameStartData
    {
        public List<ParticipantInfo> participants;
        public QuizResponse question;
    }

    [Serializable]
    public class ParticipantInfo
    {
        public long user_id;
        public string nickname;
        public int level;
        public bool is_examiner;
        public int banana_score;
    }

    [Serializable]
    public class PlayerRankInfo
    {
        public int rank;
        public string nickname;
        public int level;
        public int banana_score;
    }

    [Serializable]
    public class GameResultDto
    {
        public List<PlayerRankInfo> rankings;
    }

    [Serializable]
    public class QuizResponse
    {
        public long quiz_id;
        public string content;
    }

    [Serializable]
    public class SubmitCardRequest
    {
        public long card_id;

        public SubmitCardRequest(long cardId)
        {
            this.card_id = cardId;
        }

        public static SubmitCardRequest From(CardData data)
        {
            return new SubmitCardRequest(data.card_id);
        }
    }

    [Serializable]
    public class CardListResponse
    {
        public List<CardData> cards; // CardData == CardResponse
    }

    [Serializable]
    public class ExaminerSelectRequest
    {
        public long participant_id;

        public ExaminerSelectRequest(long participantId)
        {
            this.participant_id = participantId;
        }

        public static ExaminerSelectRequest From(CardData data)
        {
            return new ExaminerSelectRequest(data.participant_id);
        }
    }

    [Serializable]
    public class MyPageResponse
    {
        public long id;
        public string email;
        public string nickname;
        public int level;
        public int bananaxp;
    }

    [Serializable]
    public class ExaminerSelectionDto
    {
        public long participant_id;
        public string card_word;
        public string winner_nickname;
        public int new_banana_score;
    }

    [Serializable]
    public class NextRoundResponse
    {
        public long new_examiner_id;
        public string new_examiner_nickname;
        public QuizResponse quiz;
    }

    [Serializable]
    public class ErrorBody
    {
        public string status;
        public string message;
        public string timestamp;
    }

    [Serializable]
    public class WebSocketRequestMessage<T>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WebSocketMessageType type;
        public string roomCode;
        public T data;

        public WebSocketRequestMessage(WebSocketMessageType type, string roomCode, T data)
        {
            this.type = type;
            this.roomCode = roomCode;
            this.data = data;
        }
    }

    [Serializable]
    public class WebSocketMessage<T>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WebSocketMessageType type;
        public string roomCode;
        public T data;
        public string message;
    }
    
    [Serializable]
    public enum WebSocketMessageType
    {
        CONNECTED,

        CREATE_ROOM,
        ROOM_CREATED,
        JOIN_ROOM,
        ROOM_JOINED,
        USER_JOINED,
        ROOM_EXIT,

        GAME_READY, // 4명 모임 → 게임 서버 연결 안내
        CONNECT_GAME, // 클라이언트 → 게임 서버 연결 요청
        GAME_START, // 게임 시작 (4명 모두 연결 완료 시, 질문 포함)
        SUBMIT_CARD,
        CARD_SUBMITTED,
        ALL_CARDS_SUBMITTED,
        EXAMINER_SELECT, // 출제자가 카드 선택
        EXAMINER_SELECTED, // 출제자 선택 완료
        NEXT_ROUND, // 다음 라운드 시작 (새 출제자 + 새 질문)
        ROUND_END, // 게임 종료 (5점 달성)

        ERROR
    }
}