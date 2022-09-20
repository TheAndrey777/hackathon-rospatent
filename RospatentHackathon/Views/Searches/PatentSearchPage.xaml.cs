namespace RospatentHackathon.Views;

public partial class PatentSearchPage : ContentPage
{
	public PatentSearchPage()
	{
		InitializeComponent();
	}
    void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        header.Text = $"Вы выбрали: {wayPicker.SelectedItem}";
    }
}
