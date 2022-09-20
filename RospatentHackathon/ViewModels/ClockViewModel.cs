using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RospatentHackathon.ViewModels;

class ClockViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private Timer _timer;

    public DateTime DateTime
    {
        get => DateTime.Now;
    }

    public ClockViewModel()
    {
        // Update the DateTime property every second.
        _timer = new Timer(new TimerCallback((s) => OnPropertyChanged("DateTime")),
                           null, TimeSpan.Zero, TimeSpan.FromSeconds(0.05));
    }

    ~ClockViewModel() =>
        _timer.Dispose();

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}