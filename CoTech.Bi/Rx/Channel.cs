using System;
using System.Collections.Generic;

namespace CoTech.Bi.Rx {
  public class Channel<T> : IObservable<T>
  {
    private List<IObserver<T>> observers = new List<IObserver<T>>();

    internal Channel() {}
    public IDisposable Subscribe(IObserver<T> observer) {
      observers.Add(observer);
      return new Subscription(() => observers.Remove(observer));
    }

    public void SendEvent(T evt) {
      foreach (IObserver<T> observer in observers) {
          observer.OnNext(evt);
      }
    }

  }
}