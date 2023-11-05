using System.Net.Sockets;
using System.Net;

var server = new TcpListener(IPAddress.Any, 8888);

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

        for (int i = 0; i < 100; i++)
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
