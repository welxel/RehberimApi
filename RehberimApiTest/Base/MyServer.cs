using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;

namespace XNUnitTest.Base {
    public class MyServer : TestServer {
        public MyServer(IServiceProvider services) : base(services) {
        }

        public MyServer(IServiceProvider services, IFeatureCollection featureCollection) : base(services, featureCollection) {
        }

        public MyServer(IWebHostBuilder builder) : base(builder) {
        }

        public MyServer(IWebHostBuilder builder, IFeatureCollection featureCollection) : base(builder, featureCollection) {
        }

        public void Dispose() {
            int a = 0;
        }
    }
}
