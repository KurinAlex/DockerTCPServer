using System.Net;
using System.Net.Sockets;

using Utility;

var server = new TcpListener(IPAddress.Any, Data.Port);

try
{
    server.Start(16);
    Console.WriteLine("Server started, listening for connections...");

    while (true)
    {
        await ProcessClient();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    server.Stop();
}

async Task ProcessClient()
{
    try
    {
        using TcpClient client = await server.AcceptTcpClientAsync();
        using NetworkStream stream = client.GetStream();
        using var reader = new BinaryReader(stream);

        string name = reader.ReadString();
        Console.WriteLine($"{name} connected");

        for (int i = 0; i < Data.MessagesCount; i++)
        {
            string message = reader.ReadString();
            Console.WriteLine($"{name}: {message}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
