using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface ICancellable
{
    public CancellationTokenSource Cancellation { get; }
}
