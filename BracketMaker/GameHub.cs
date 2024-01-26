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
          var groupInfo = new GroupInfo
          {
               HostConnectionID = Context.ConnectionId,
               GroupID = Guid.NewGuid().ToString()
          };
          gameService.GameHosts.Add(groupInfo.GroupID, new GameInfo{ HostConnectionID = Context.ConnectionId});
          await Clients.Caller.SendAsync("onGroupCreated", groupInfo);
     }

     public async Task JoinGroup(string groupId, string userName)
     {
          var joinRequest = new JoinRequest()
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
          var gameExists = gameService.GameHosts.TryGetValue(groupID, out var gameInfo);
          if (gameExists)
          {
               gameService.GameHosts.Remove(groupID);
          }
     }

     public async Task SendScore(GameAnswer gameAnswer)
     {
          var gameInfo = gameService.GameHosts[gameAnswer.GameID];
          var player = gameInfo.Players.Single(p => p.UserName == gameAnswer.UserName);
          var question = gameInfo.Questions[gameAnswer.QuestionID];

          if ((question.Answer & gameAnswer.Answer) != 0)
          {
               player.Score++;
               await Clients.Caller.SendAsync("goodAnswer", player.Score);
          }
          else
          {
               await Clients.Caller.SendAsync("wrongAnswer", player.Score);
          }
          await Clients.Client(gameInfo.HostConnectionID).SendAsync("playerAnswered", player.UserName);
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
          await Clients.Client(gameInfo!.HostConnectionID).SendAsync("nextQuestion", gameInfo.Questions[nextQuestionId]);
     }
}