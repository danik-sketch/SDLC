namespace PasswordGeneratorApp;

public class Controller
{
    private readonly Model _model;

    public Controller(Model model)
    {
        _model = model;
    }

    public void HandleInputData(IWin32Window owner)
    {
        using (var dialog = new InputDialogForm(_model.CurrentSettings))
        {
            if (dialog.ShowDialog(owner) == DialogResult.OK) _model.SaveSettings(dialog.Settings);
        }
    }

    public void HandleGenerate()
    {
        _model.Generate();
    }
}