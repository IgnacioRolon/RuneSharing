using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace RuneSharing
{
    class LCUConnector
    {
        private bool isConnected = false;
        private string process;
        private string processID;
        private string port;
        private const string user = "riot";
        private string password;
        private string protocol;

        public enum RequestType
        {
            GET,
            POST,
            DELETE,
            PUT,
            PATCH
        }

        public bool Connect(string gamePath)
        {
            string lockfilePath = gamePath + "/lockfile";
            if (File.Exists(lockfilePath) && !isConnected)
            {
                File.Copy(lockfilePath, lockfilePath + "runeSharing", true);
                string lockfileData = File.ReadAllText(lockfilePath + "runeSharing");
                string[] parsedData = lockfileData.Split(':');
                process = parsedData[0];
                processID = parsedData[1];
                port = parsedData[2];
                password = parsedData[3];
                protocol = parsedData[4];
                isConnected = true;

                HttpConnections.Initialize(this.GetAPIPath(), this.GetAuthorization());
                return true;                
            }
            else
            {                
                return false;
            }
        }

        public LCUConnector(string gamePath)
        {
            if(!this.Connect(gamePath))
            {
                throw new LeagueNotRunningException("The Client instance could not be found. This could be either because the client is not running, or because the path is invalid.");
            }
        }

        public string GetAuthorization()
        {
            string authString = user + ":" + password;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
        }
        
        public string GetFullAuth()
        {
            return "Basic " + this.GetAuthorization();
        }

        public string GetAPIPath()
        {
            return protocol + "://127.0.0.1:" + port + "/";
        }

        public async Task<RunePage> APICall(RequestType type, string request, RunePage runes)
        {
            RunePage response = null;
            if(type == RequestType.GET)
            {
                response = await HttpConnections.GetRunes(request);
            }else if(type == RequestType.POST)
            {
                await HttpConnections.PostRunesAsync(request, runes);
            }else if(type == RequestType.DELETE)
            {
                await HttpConnections.DeleteRunes(request);
            }

            return response;
        }
    }
}
