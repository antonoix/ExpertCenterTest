using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnimatedButton : MonoBehaviour, ICancellable
{
    public event Action OnClicked;
    public CancellationTokenSource Cancellation { get; private set; }

    private CancellationTokenSource _doubleClick = new CancellationTokenSource();

    public void Init()
    {
        print("awake");
        GetComponent<Button>().onClick.AddListener(async () =>
        {
            print("A");
            if (_doubleClick.Token.IsCancellationRequested) return;
            _doubleClick?.Cancel();

            await Task.Delay(GameSettings.UI.ButtonsTransitionDurationMs);
            if (Cancellation?.Token.IsCancellationRequested == true) return;
            OnClicked?.Invoke();

            _doubleClick = new CancellationTokenSource();
        });
    }

    private void OnDisable()
    {
        Cancellation?.Cancel();
    }
}
