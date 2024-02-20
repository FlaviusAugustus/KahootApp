using BracketMaker.ClientMethods;
using BracketMaker.Models;
using BracketMaker.Services;
using Microsoft.AspNetCore.SignalR;
namespace BracketMaker;

public class GameHub(IGameService gameService) : Hub<IGameTypedHub>
{
     public async Task CreateGroup(string roomName)
     {
          gameService.AddGame(Context.ConnectionId);
          await Clients.Caller.OnGroupCreated(new GroupInfo
          {
               HostConnectionID = Context.ConnectionId, 
               GroupID = gameService.GenerateGroupID()
          });
     }

     public async Task JoinGroup(string groupId, string userName)
     {
          var joinRequest = new JoinRequest
          {
               GroupID = groupId,
               UserName = userName,
               ConnectionID = Context.ConnectionId
          };
          
          var joinResult = gameService.JoinGroup(joinRequest);
          if (!joinResult.Success)
          {
               await Clients.Caller.FailedToJoinGame(joinResult.Message);
               return;
          }
          
          await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

          var hostConnectionId = gameService.GetHost(groupId);
          await Clients.Client(hostConnectionId).PlayerJoined(userName);
          await Clients.Caller.GameJoined(userName);
     }

     public void StopGame(string groupID)
     {
          gameService.RemoveGame(groupID);
     }

     public void StartGame(string groupID)
     {
          gameService.StartGame(groupID);
     }

     public async Task SendScore(GameAnswer gameAnswer)
     {
          var isAnswerCorrect = gameService.ProcessAnswer(gameAnswer);
          var gameInfo = gameService.GetGameState(gameAnswer.GameID);
          
          await Clients.Client(gameInfo.HostConnectionID).PlayerAnswered(isAnswerCorrect.ToString());
          await Clients.Caller.ReceiveQuestionResult(isAnswerCorrect.ToString());
     }

     public async Task GetNextQuestion(string groupID, int questionID = -1)
     {
          var gameExists = gameService.GameHosts.TryGetValue(groupID, out var gameInfo);
          if (!gameExists)
          {
               return;
          }
          var nextQuestionId = questionID++;
          await Clients.Client(gameInfo!.HostConnectionID).NextQuestion(gameInfo.Quiz.Questions[nextQuestionId]);
     }
}