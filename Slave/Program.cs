using Discord;
using Discord.Commands;
using Discord.WebSocket;


public class Program
{
    private static DiscordSocketClient? _client;
    private static CommandService? command;

    public static async Task Main()
    {
        _client = new DiscordSocketClient();
        command = new CommandService();
        var _config = new DiscordSocketConfig { MessageCacheSize = 100 };

        _client.Log += Log;
        command.Log += Log;

        var token = File.ReadAllText("token.txt");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private static Task Log(LogMessage message)
    {
        if (message.Exception is CommandException cmdException)
        {
            Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
                + $" failed to execute in {cmdException.Context.Channel}.");
            Console.WriteLine(cmdException);
        }
        else
            Console.WriteLine($"[General/{message.Severity}] {message}");


        return Task.CompletedTask;
    }

    private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
    {

    }
}