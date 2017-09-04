using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace CoTech.Bi.Rx {
  public class EventEmitter {

    private ServiceContainer container = new ServiceContainer();

    public Channel<T> CreateChannel<T>() {
      if(container.GetService(typeof(Channel<T>)) != null) {
        throw new Exception("channel already exists");
      }
      Channel<T> channel = new Channel<T>();
      container.AddService(typeof(Channel<T>), channel);
      return channel;
    }

    public IDisposable Subscribe<T>(IObserver<T> observer) {
      Channel<T> channel = (Channel<T>)container.GetService(typeof(Channel<T>));
      if(channel == null){
        throw new Exception("channel not found");
      }
      return channel.Subscribe(observer);
    }

  }

}