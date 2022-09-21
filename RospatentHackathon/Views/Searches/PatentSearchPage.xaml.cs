namespace RospatentHackathon.Views;

public partial class PatentSearchPage : ContentPage
{
    Label label;
	public PatentSearchPage()
	{
		InitializeComponent();
	}
    void DateSelectedOT(object sender, DateChangedEventArgs e)
    {
        label.Text = $"Вы выбрали {e.NewDate.ToString("d")}";
    }
    //для передачи в джейсон нужно намутить кастомный формат в e.NewDate.Tostring обращайся если что - объясню
    void DateSelectedDO(object sender, DateChangedEventArgs e)
    {
        label.Text = $"Вы выбрали {e.NewDate.ToString("d")}";
    }
}
