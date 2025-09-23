using FireTalk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTalk.Services
{
    public interface IFireTalkService
    {
        Task<List<MessageGroupModel>> GetUserGroups(string userId);
        Task<bool> CreateMessageGroup(MessageGroupModel payload);
        Task<bool> SaveUserDataAsync(UserModel user);
        Task<UserModel?> LoginAsync(string email, string password);
        Task<bool> CreateChatMessage(ChatModel payload);
        Task<List<UserModel>> GetAllUsers();
        Task<List<ChatModel>> GetUserChats(string groupId, int pageSize);
        Task<List<UserModel>> GetUserDetailsByIds(List<string> userIds);
    }
}
