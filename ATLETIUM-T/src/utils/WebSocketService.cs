using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATLETIUM_T;

public class WebSocketService
{
    private ClientWebSocket _webSocket;
    private CancellationTokenSource _cancellationTokenSource;

    public event Action<string> MessageReceived;

    public async Task ConnectAsync(string uri)
    {
        _webSocket = new ClientWebSocket();
        _cancellationTokenSource = new CancellationTokenSource();

        try
        {
            await _webSocket.ConnectAsync(new Uri(uri), _cancellationTokenSource.Token);
            StartReceivingMessages();
        }
        catch (Exception ex)
        {
            new ToastMessage().ShortToast($"WebSocket connection failed: {ex.Message}");
        }
    }

    private async void StartReceivingMessages()
    {
        var buffer = new byte[1024 * 4];

        try
        {
            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cancellationTokenSource.Token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    MessageReceived?.Invoke(message);
                }
            }
        }
        catch (Exception ex)
        {
            new ToastMessage().ShortToast($"Error receiving message: {ex.Message}");
        }
    }

    public async Task SendMessageAsync(string message)
    {
        if (_webSocket?.State == WebSocketState.Open)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, _cancellationTokenSource.Token);
        }
    }

    public async Task DisconnectAsync()
    {
        if (_webSocket != null)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            _webSocket.Dispose();
        }

        _cancellationTokenSource?.Cancel();
    }
}