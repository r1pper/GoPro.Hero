using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GoPro.Hero.Api.Commands
{
    class Commando
    {
        public static CommandResponse Send(CommandRequest command)
        {
            var request = HttpWebRequest.Create(command.GetUri()) as HttpWebRequest;
            //request.KeepAlive=true;
            //request.ProtocolVersion=HttpVersion.Version11;
            //request.SendChunked = true;
            //request.TransferEncoding="ISO-8859-1";

            var asyncResponse=request.BeginGetResponse(null,null);
            asyncResponse.AsyncWaitHandle.WaitOne();
            
            using (var response =request.EndGetResponse(asyncResponse)  as HttpWebResponse)
            {
                var stream = response.GetResponseStream();
                var buffer = new byte[response.ContentLength];
                stream.Read(buffer, 0, buffer.Length);
                stream.Dispose();
                return CommandResponse.Create(buffer);
            }
        }
    }
}
