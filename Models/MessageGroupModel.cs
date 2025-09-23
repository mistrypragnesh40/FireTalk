using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTalk.Models
{
    [FirestoreData]
    public class MessageGroupModel
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Title { get; set; }

        [FirestoreProperty]
        public string LastSentMessage { get; set; }

        [FirestoreProperty]
        public Timestamp LastSentMssageTime { get; set; }

        [FirestoreProperty]
        public string OwnerId { get; set; }

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; }

        [FirestoreProperty]
        public List<string> Members { get; set; }
    }

}
