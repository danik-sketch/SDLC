namespace PasswordGeneratorApp;

public class InputDialogForm : Form
{
    private Button btnCancel;
    private Button btnSave;
    private CheckBox chkDigits;
    private CheckBox chkLower;
    private CheckBox chkSpecial;
    private CheckBox chkUpper;
    private NumericUpDown numCount;
    private NumericUpDown numLength;

    public InputDialogForm(PasswordSettings initialSettings)
    {
        InitializeComponent();
        LoadSettings(initialSettings);
    }

    public PasswordSettings Settings { get; private set; }

    private void InitializeComponent()
    {
        Text = "Ввод параметров генерации";
        Size = new Size(300, 320);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        var lblLength = new Label { Text = "Длина пароля:", Left = 20, Top = 20, Width = 120 };
        numLength = new NumericUpDown { Left = 150, Top = 18, Width = 100, Minimum = 1, Maximum = 128 };

        var lblCount = new Label { Text = "Количество:", Left = 20, Top = 50, Width = 120 };
        numCount = new NumericUpDown { Left = 150, Top = 48, Width = 100, Minimum = 1, Maximum = 100 };

        chkUpper = new CheckBox { Text = "Заглавные буквы (A-Z)", Left = 20, Top = 85, Width = 200 };
        chkLower = new CheckBox { Text = "Строчные буквы (a-z)", Left = 20, Top = 115, Width = 200 };
        chkDigits = new CheckBox { Text = "Цифры (0-9)", Left = 20, Top = 145, Width = 200 };
        chkSpecial = new CheckBox { Text = "Спец. символы (!@#...)", Left = 20, Top = 175, Width = 200 };

        btnSave = new Button { Text = "Принять", Left = 30, Top = 220, Width = 100, DialogResult = DialogResult.OK };
        btnCancel = new Button
            { Text = "Отмена", Left = 150, Top = 220, Width = 100, DialogResult = DialogResult.Cancel };

        btnSave.Click += BtnSave_Click;

        Controls.AddRange(lblLength, numLength, lblCount, numCount, chkUpper, chkLower, chkDigits, chkSpecial, btnSave,
            btnCancel);
    }

    private void LoadSettings(PasswordSettings settings)
    {
        numLength.Value = settings.Length;
        numCount.Value = settings.Count;
        chkUpper.Checked = settings.IncludeUppercase;
        chkLower.Checked = settings.IncludeLowercase;
        chkDigits.Checked = settings.IncludeDigits;
        chkSpecial.Checked = settings.IncludeSpecial;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        Settings = new PasswordSettings
        {
            Length = (int)numLength.Value,
            Count = (int)numCount.Value,
            IncludeUppercase = chkUpper.Checked,
            IncludeLowercase = chkLower.Checked,
            IncludeDigits = chkDigits.Checked,
            IncludeSpecial = chkSpecial.Checked
        };
    }
}