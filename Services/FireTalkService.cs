using FireTalk.Models;
using Google.Cloud.Firestore;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTalk.Services
{
    public class FireTalkService : IFireTalkService
    {
        private readonly FirestoreDb _fireStoreDb;
        public FireTalkService(FireStoreService fireStoreService)
        {
            _fireStoreDb = fireStoreService.Db;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var usersRef = _fireStoreDb.Collection("users");
            var snapShot = await usersRef.GetSnapshotAsync();
            return snapShot.Documents.Select(d => d.ConvertTo<UserModel>()).ToList();
        }


        public async Task<UserModel?> LoginAsync(string email, string password)
        {
            var usersRef = _fireStoreDb.Collection("users");
            var query = usersRef.WhereEqualTo("Email", email);
            var snapShot = await query.GetSnapshotAsync();

            var doc = snapShot.Documents.FirstOrDefault();

            if (doc != null)
            {
                var user = doc.ConvertTo<UserModel>();
                if (user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        private async Task<bool> IsUserExistAsync(string email)
        {
            var usersRef = _fireStoreDb.Collection("users");
            var query = usersRef.WhereEqualTo("Email", email);
            var snapShot = await query.GetSnapshotAsync();
            return snapShot.Count > 0;
        }

        public async Task<List<UserModel>> GetUserDetailsByIds(List<string> userIds)
        {
            var usersRef = _fireStoreDb.Collection("users");
            var query = usersRef.WhereIn("Id", userIds);
            var snapShot = await query.GetSnapshotAsync();
            return snapShot.Documents.Select(d => d.ConvertTo<UserModel>()).ToList();
        }


        public async Task<bool> SaveUserDataAsync(UserModel user)
        {
            if (await IsUserExistAsync(user.Email))
            {
                return false;
            }

            var docRef = _fireStoreDb.Collection("users").Document(user.Id);
            await docRef.SetAsync(user);
            return true;
        }


        public async Task<bool> CreateMessageGroup(MessageGroupModel payload)
        {

            var docRef = _fireStoreDb.Collection("groups").Document(payload.Id);
            await docRef.SetAsync(payload);
            return true;
        }

        public async Task<bool> CreateChatMessage(ChatModel payload)
        {

            var docRef = _fireStoreDb.Collection("chats").Document(payload.Id);
            await docRef.SetAsync(payload);
            return true;
        }


        public async Task<List<ChatModel>> GetUserChats(string groupId, int pageSize)
        {
            var chatQuery = _fireStoreDb.Collection("chats")
                            .WhereEqualTo("GroupId", groupId)
                            .OrderByDescending("CreatedAt")
                            .Limit(pageSize);

            var chatSnapshot = await chatQuery.GetSnapshotAsync();
            return chatSnapshot.Documents.Select(d => d.ConvertTo<ChatModel>()).ToList();



        }


        public async Task<List<MessageGroupModel>> GetUserGroups(string userId)
        {
            var docRef = _fireStoreDb.Collection("groups");
            var query = docRef.WhereArrayContains("Members", userId);
            var snapShot = await query.GetSnapshotAsync();
            return snapShot.Documents.Select(d => d.ConvertTo<MessageGroupModel>()).ToList();
        }


    }
}
