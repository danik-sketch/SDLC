namespace PasswordGeneratorApp;

public partial class Form1 : Form
{
    private readonly Controller _controller;
    private readonly Model _model;
    private Button btnGenerate;

    private Button btnInputData;
    private Label lblParamsInfo;
    private TextBox txtResults;

    public Form1()
    {
        InitializeComponent();

        SetupUI();

        _model = new Model();
        _controller = new Controller(_model);

        _model.DataChanged += OnModelDataChanged;
        _model.ValidationError += OnModelValidationError;

        OnModelDataChanged();
    }

    private void SetupUI()
    {
        Text = "Генератор паролей";
        Size = new Size(450, 420);
        StartPosition = FormStartPosition.CenterScreen;

        btnInputData = new Button
        {
            Text = "Ввести данные",
            Left = 20,
            Top = 20,
            Width = 140,
            Height = 35
        };
        btnInputData.Click += (s, e) => _controller.HandleInputData(this);

        btnGenerate = new Button
        {
            Text = "Сгенерировать",
            Left = 170,
            Top = 20,
            Width = 140,
            Height = 35
        };
        btnGenerate.Click += (s, e) => _controller.HandleGenerate();

        lblParamsInfo = new Label
        {
            Left = 20,
            Top = 70,
            Width = 400,
            Height = 40,
            Text = "Параметры: данные еще не введены."
        };

        txtResults = new TextBox
        {
            Left = 20,
            Top = 120,
            Width = 390,
            Height = 230,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true
        };

        Controls.Add(btnInputData);
        Controls.Add(btnGenerate);
        Controls.Add(lblParamsInfo);
        Controls.Add(txtResults);
    }

    private void OnModelDataChanged()
    {
        var s = _model.CurrentSettings;

        lblParamsInfo.Text = $"Длина: {s.Length} | Кол-во: {s.Count} | " +
                             $"Наборы: {(s.IncludeUppercase ? "[A-Z] " : "")}" +
                             $"{(s.IncludeLowercase ? "[a-z] " : "")}" +
                             $"{(s.IncludeDigits ? "[0-9] " : "")}" +
                             $"{(s.IncludeSpecial ? "[Spec] " : "")}";

        txtResults.Lines = _model.GeneratedPasswords.ToArray();
    }
private void OnModelValidationError(string errorMessage)
    {
        MessageBox.Show(errorMessage, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

}