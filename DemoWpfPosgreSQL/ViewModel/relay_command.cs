using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoWpfPosgreSQL.ViewModel
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object> execute_;
		private readonly Predicate<object> can_execute_;

		public RelayCommand(Action<object> execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action<object> execute, Predicate<object> can_execute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			execute_ = execute;
			can_execute_ = can_execute;
		}

		public bool CanExecute(object parameter)
		{
			return can_execute_ == null ? true : can_execute_(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			execute_(parameter);
		}
	}
}
