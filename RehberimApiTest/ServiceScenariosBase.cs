using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using RehberimApi;
using XNUnitTest.Base;

namespace XServiceUnitTest {
    public class ServiceScenariosBase
    {
        public static ServiceScenariosBase InstanceBase = new ServiceScenariosBase();
        public MyServer Instance = null;
        public HttpClient client;
        public int UserId { get; set; }

        public HttpClient CreateClient()
        {
            CreateServer();
            if (client!=null)
            {
                return client;
            }
             client = CreateServer().CreateClient();
             return client;
        }


        public MyServer CreateServer() {

            if (Instance != null) {
                return Instance;
            }

            var path = Assembly.GetAssembly(typeof(Startup))
                .Location;
            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .UseStartup<Startup>();
            Instance = new MyServer(hostBuilder);

            return Instance;
        }


        public static class Get {
            public static string GetPerson() {
                return $"api/personoperation/getpersons";
            }
            public static string GetUserById(int userId) {
                return $"api/personoperation/getuserbyid/{userId}";
            }
        }

        public static class Post {
            public static string AddPerson() {
                return $"api/personoperation/addperson";
            }
            public static string AddPersonInfo() {
                return $"api/personinfo/addpersoninfo";
            }
        }

        public static class Put
        {
        }

        public static class Delete {
            public static string DeletePersonelInfo(int userId) {
                return $"api/personinfo/deletepersoninfo/{userId}";
            }
            public static string DeletePersonel(int userId) {
                return $"api/personoperation/deleteperson/{userId}";
            }
        }
    }
}

