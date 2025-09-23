using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireTalk.Services
{
    public class FireStoreService
    {
        private readonly FirestoreDb _firestoreDb;

        public FireStoreService()
        {
            var stream = FileSystem.OpenAppPackageFileAsync("firebase_admin_sdk.json").Result;
            using var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            var clientBuilder = new FirestoreClientBuilder
            {
                JsonCredentials = contents
            };

            _firestoreDb = FirestoreDb.Create("employeedetails-53bc0", clientBuilder.Build());
        }

        public FirestoreDb Db => _firestoreDb;
       
    }
}
