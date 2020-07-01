using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace UpdateICA
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = CApiConnector.AcquireToken("apiuser1@example.com", "Password1!");
            //var token = CApiConnector.AcquireToken("anders.dahlgren@tillvaxtverket.se", "Tua123!");
            
            var serviceList = CApiConnector.FindService(9, token);

            //provider = FindProvider(newProvider.Id, token);
            //System.Diagnostics.Debug.Assert(newProvider.Name == provider.Name);*/
        }
    }
}
