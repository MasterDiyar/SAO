using Godot;
using System; // Необходимо для Action

public partial class Twenty : Label
{
    public event Action Timeout;

    [Export] public float TimeLeft { get; set; } = 1200f; 
    
    private bool _isRunning = true;

    public override void _Ready()
    {
        UpdateLabelText();
    }

    public override void _Process(double delta)
    {
        if (!_isRunning) return;

        TimeLeft -= (float)delta;

        if (TimeLeft <= 0) {
            TimeLeft = 0;
            _isRunning = false; 
            UpdateLabelText();
            
            Timeout?.Invoke(); 
        } else
            UpdateLabelText();
        
    }

    private void UpdateLabelText()
    {
        int minutes = Mathf.FloorToInt(TimeLeft / 60.0f);
        int seconds = Mathf.FloorToInt(TimeLeft % 60.0f);

        Text = $"{minutes:00}:{seconds:00}";
    }

    public void ResetTimer()
    {
        TimeLeft = 1200f;
        _isRunning = true;
    }
}