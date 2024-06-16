using System.ComponentModel;

namespace MauiAppTest.ViewModels
{
	public class Car : INotifyPropertyChanged
	{
		long _id;
		public long Id
		{
			get => _id;
			set
			{
				if (_id == value)
					return;
				_id = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
			}
		}

		string _brand;
		public string Brand
		{
			get => _brand;
			set
			{
				if (_brand == value)
					return;
				_brand = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Brand)));
			}
		}

		string _model;
		public string Model
		{
			get => _model;
			set
			{
				if (_model == value)
					return;
				_model = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Model)));
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
