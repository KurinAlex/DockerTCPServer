using System.Net;
using System.Net.Sockets;

using Utility;

try
{
    Console.Write("Enter your name: ");
    string? name = Console.ReadLine();

    using var client = new TcpClient();
    client.Connect(IPAddress.Loopback, Data.Port);

    using var stream = client.GetStream();
    using var writer = new BinaryWriter(stream);

    SendMessage(writer, name);

    for (int i = 0; i < Data.MessagesCount; i++)
    {
        string message = $"Message {i + 1}";
        SendMessage(writer, message);
        Console.WriteLine($"Message sent: {message}");
    }

    client.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static void SendMessage(BinaryWriter writer, string? message)
{
    if (message == null)
    {
        return;
    }
    writer.Write(message);
    writer.Flush();
}
