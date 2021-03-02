using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WheatherApiApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM _vm { get; set; }

        public SearchCommand(WeatherVM vm)
        {
            _vm = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove => CommandManager.RequerySuggested += value;
            //both using is ok one of is normal the other one is syntax style
        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;
            if (string.IsNullOrWhiteSpace(query)) return false;
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.MakeQuery();
        }

        
    }
}
