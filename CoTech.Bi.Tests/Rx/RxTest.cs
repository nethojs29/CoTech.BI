using System;
using CoTech.Bi.Rx;
using Xunit;

namespace CoTech.Bi.Tests
{
    public class RxTest
    {
        EventEmitter emitter = new EventEmitter();
        public RxTest()
        {

        }

        [Fact]
        public void SubscribeToIntChannelAndReceive24()
        {
        //Given
        Channel<Int32> intChannel = emitter.CreateChannel<Int32>();
        SaveValueObserver<Int32> observer = new SaveValueObserver<Int32>();
        IDisposable subscription = emitter.Subscribe<int>(observer);
        //When
        intChannel.SendEvent(24);
        //Then
        Assert.Equal(observer.value, 24);
        subscription.Dispose();
        }
    }

    class SaveValueObserver<T> : IObserver<T> {
      public T value;

      public void OnNext(T value) {
        this.value = value;
      }

      public void OnError(Exception e) { }

      public void OnCompleted() {}

    }
}