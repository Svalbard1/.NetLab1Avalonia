using AvaloniaStack.Models;
using ReactiveUI;
using System;

namespace AvaloniaStack.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly Stack<string> _stack = new();
        private string _input = string.Empty;
        private string _status = string.Empty;

        public string Input
        {
            get => _input;
            set => this.RaiseAndSetIfChanged(ref _input, value);
        }

        public string TopItem => _stack.Top?.ToString() ?? "[Пусто]";
        public int Count => _stack.Count;
        public bool IsEmpty => _stack.IsEmpty;
        
        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public void PushCommand()
        {
            if (!string.IsNullOrWhiteSpace(Input))
            {
                _stack.Push(Input);
                Input = string.Empty;
                UpdateProperties();
                Status = "Элемент добавлен";
            }
        }

        public void PopCommand()
        {
            try
            {
                _stack.Pop();
                UpdateProperties();
                Status = "Элемент извлечен";
            }
            catch (InvalidOperationException)
            {
                Status = "Ошибка: стек пуст!";
            }
        }

        public void ClearCommand()
        {
            _stack.Clear();
            UpdateProperties();
            Status = "Стек очищен";
        }

        private void UpdateProperties()
        {
            this.RaisePropertyChanged(nameof(TopItem));
            this.RaisePropertyChanged(nameof(Count));
            this.RaisePropertyChanged(nameof(IsEmpty));
        }
    }
}