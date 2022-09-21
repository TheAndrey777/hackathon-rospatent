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
        label.Text = $"�� ������� {e.NewDate.ToString("d")}";
    }
    //��� �������� � ������� ����� �������� ��������� ������ � e.NewDate.Tostring ��������� ���� ��� - �������
    void DateSelectedDO(object sender, DateChangedEventArgs e)
    {
        label.Text = $"�� ������� {e.NewDate.ToString("d")}";
    }
}
