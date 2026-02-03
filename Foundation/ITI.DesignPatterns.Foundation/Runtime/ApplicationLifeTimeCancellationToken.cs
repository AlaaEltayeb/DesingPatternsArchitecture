using System.Threading;
using UnityEngine;

namespace ITI.DesignPatterns.Foundation.Runtime
{
    public readonly struct ApplicationLifeTimeCancellationToken
    {
        public static CancellationToken Token => Application.exitCancellationToken;
    }
}