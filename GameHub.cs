using BracketMaker.Models;
using BracketMaker.Services;
using LanguageExt;
using LanguageExt.Pipes;
using Microsoft.AspNetCore.SignalR;
namespace BracketMaker;

public class GameHub(IGameService gameService) : Hub
{
     public async Task CreateGroup(string roomName)
     {
          gameService.GameHosts.Remove(Context.ConnectionId); 
          
          var groupInfo = new GroupInfo
          {
               HostConnectionID = Context.ConnectionId,
               GroupID = Guid.NewGuid().ToString()
          };
          
          gameService.GameHosts.Add(groupInfo.HostConnectionID, new GameInfo{ HostConnectionID = Context.ConnectionId});
          await Clients.Caller.SendAsync("onGroupCreated", groupInfo);
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
               await Clients.Caller.SendAsync("failedToJoin", joinResult.Message);
               return;
          }
          
          var gameInfo = gameService.GameHosts[groupId];
          await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
          await Clients.Client(gameInfo!.HostConnectionID).SendAsync("playerJoined", userName);
          await Clients.Caller.SendAsync("joinedGame", userName);
     }

     public void StopGame(string groupID)
     {
          gameService.GameHosts.Remove(groupID);
     }

     public void StartGame(string groupID)
     {
          gameService.GameHosts[groupID].IsStarted = true;
     }

     public async Task SendScore(GameAnswer gameAnswer)
     {
          var isAnswerCorrect = gameService.ProcessAnswer(gameAnswer);
          var gameInfo = gameService.GameHosts[gameAnswer.GameID];
          await Clients.Client(gameInfo.HostConnectionID).SendAsync("playerAnswered", isAnswerCorrect);
          await Clients.Caller.SendAsync("answerResult", isAnswerCorrect);
     }

     public async Task GetNextQuestion(string groupID, int questionID = -1)
     {
          var gameExists = gameService.GameHosts.TryGetValue(groupID, out var gameInfo);
          if (!gameExists)
          {
               await Clients.Caller.SendAsync("groupDoesNotExist", groupID);
               return;
          }
          var nextQuestionId = questionID++;
          await Clients.Client(gameInfo!.HostConnectionID).SendAsync("nextQuestion", gameInfo.Quiz.Questions[nextQuestionId], nextQuestionId);
     }
}