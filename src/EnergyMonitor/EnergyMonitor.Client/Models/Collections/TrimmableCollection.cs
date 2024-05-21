using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace EnergyMonitor.Client.Models.Collections
{
    public class TrimmableCollection<T> : ObservableCollection<T>
    {
        public int? Maximum { get; set; } = null;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            if (Maximum != null && this.Count > Maximum)
            {
                base.RemoveAt(0);
            }

        }
    }
}
