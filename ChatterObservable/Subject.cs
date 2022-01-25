using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatterObservable
{
    public abstract class Subject
    {
        public List<IObserver> observers = new();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
            observers.ForEach(x => x.ClientAttach());
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
            observers.ForEach(x => x.ClientDetach());
        }

        public void Notify() => observers.ForEach(o => o.Update());
    }
}
