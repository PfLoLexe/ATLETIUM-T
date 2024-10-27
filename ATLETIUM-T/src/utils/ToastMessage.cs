using System.Threading;
using CommunityToolkit.Maui.Core;
using Toast = CommunityToolkit.Maui.Alerts.Toast;


namespace ATLETIUM_T;

public class ToastMessage
{
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public async void ShortToast(string message, double size = 14)
    {
        ToastDuration duration = ToastDuration.Short;
        var toast = Toast.Make(message, duration, size);

        await toast.Show(_cancellationTokenSource.Token);
    }

    public async void LongToast(string message, double size = 14)
    {
        ToastDuration duration = ToastDuration.Long;
        var toast = Toast.Make(message, duration, size);

        await toast.Show(_cancellationTokenSource.Token);
    }
}