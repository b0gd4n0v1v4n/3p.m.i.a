using System;
using System.Windows;

namespace AIMP_v3._0.ViewModel.CardsTrancport
{
    public class PreCheckCardTrancportViewModel : BaseViewModel
    {
        public bool Enable { get; set; }
        private DateTime _date;
        public DateTime Date { get { return _date; } set { _date = value; OnPropertyChanged("DateString"); } }
        public string DateString { get
            {
                return Date.ToString(Aimp.Model.DataFormats.DateFormat);
            } }
        public string Name { get; set; }
        public decimal Summ { get; set; }
        public decimal PriceForClient { get; set; }
        public bool Paid { get; set; }
        public bool Card { get; set; }
        public int CardTrancportId { get; set; }
        public bool IsAdd { get; private set; }
        public Command CloseCommand
        {
            get
            {
                return new Command((win) =>
                {
                    var window = (win as Window);


                    if (window != null)
                    {
                        window.Close();
                    }
                });
            }
        }
        public Command OkCommand
        {
            get
            {
                return new Command((win) =>
                {
                    var window = (win as Window);

                    IsAdd = true;
                    if (window != null)
                    {
                        window.Close();
                    }
                });
            }
        }
    }
}
